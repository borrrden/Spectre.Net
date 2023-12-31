// -----------------------------------------------------------------------
// <copyright file="SpectreSiteKey_v3.cs" company="Jim Borden">
// Copyright (c) Jim Borden. All rights reserved.
// Licensed under the GPL-3.0 license. See LICENSE.md file in the project root for full license information.
// </copyright>
// -----------------------------------------------------------------------

using System.Security.Cryptography;
using System.Text;
using Microsoft.Extensions.Logging;

namespace Spectre.Net.Api.Algorithms;

/// <summary>
/// The v3 implementation of the site key derivation (which at the time of writing is detailed in https://spectre.app/spectre-algorithm.pdf).
/// </summary>
internal sealed class SpectreSiteKey_v3 : ISpectreSiteKey
{
    private readonly ILogger<SpectreSiteKey_v3> _logger = SpectreUtil.LoggerFactory.CreateLogger<SpectreSiteKey_v3>();
    private readonly byte[] _keyMaterial;

    public SpectreSiteKey_v3(byte[] keyMaterial, string siteName, SpectreCounter keyCounter, SpectreKeyPurpose keyPurpose, string? keyContext)
    {
        var scope = keyPurpose.ToPurposeScope();
        _logger.LogTrace("keyScope: {scope}", scope);
        if (keyCounter == SpectreCounter.TOTP) {
            throw new NotImplementedException();
        }

        _logger.LogTrace(
            "siteSalt: keyScope={scope} | #siteName={siteName:X9} | siteName={siteLength} | keyCounter={keyCounter:X9} | #keyContext={keyContextLength:X9} | keyContext={keyContext}",
            scope,
            siteName.Length,
            siteName,
            ((int)keyCounter).ToString("X9"),
            keyContext?.Length ?? 0,
            keyContext);

        // This was trickier than it seemed because all of this is endian sensitive and took some
        // trial and error to figure out that the .NET classes do the "wrong" endian.  This needs
        // to be big endian to match the reference.  For example, with a null key context, the salt
        // will look like this for an authentication password with a counter of 1:
        //
        // 63 6F 6D 2E 6C 79 6E 64 69 72 2E 6D 61 73 74 65 72 70 61 73 73 77 6F 72 64 00 00 00 06 67 6F 6F 67 6C 65 00 00 00 01
        // [com.lyndir.masterpassword 6 google 1]
        using var siteSaltStream = new MemoryStream();
        siteSaltStream.Write(Encoding.ASCII.GetBytes(scope));
        siteSaltStream.Write(BitConverter.GetBytes(siteName.Length).Reverse().ToArray());
        siteSaltStream.Write(Encoding.ASCII.GetBytes(siteName));
        siteSaltStream.Write(BitConverter.GetBytes((uint)keyCounter).Reverse().ToArray());
        if (keyContext != null) {
            siteSaltStream.Write(BitConverter.GetBytes(keyContext.Length).Reverse().ToArray());
            siteSaltStream.Write(Encoding.ASCII.GetBytes(keyContext));
        }

        var siteSalt = siteSaltStream.ToArray();
        _logger.LogTrace("siteSalt.id: {siteSaltID}", Convert.ToHexString(SHA256.HashData(siteSalt)));

        // The salt is not used as a salt, per se, but rather used as the M in HMAC (message)
        // derived using the key material generated from the user key.
        using var hmac = new HMACSHA256(keyMaterial);
        _keyMaterial = hmac.ComputeHash(siteSalt);
        ID = SpectreUtil.StringSHA256(_keyMaterial);
        _logger.LogTrace("siteKey.id: {siteKeyID}", this);
    }

    /// <inheritdoc />
    public string ID { get; }

    /// <inheritdoc />
    public SpectreAlgorithmVersion Version => SpectreAlgorithmVersion.V3;

    /// <inheritdoc />
    public string CreatePassword(SpectreResultType type)
    {
        // Deliciously simple, just iterate over a pre-defined template and use the input
        // byte of the derived key to choose a character class (such as alphanumeric, etc)
        var seedByte = _keyMaterial[0];
        var template = SpectreType.GetTemplate(type, seedByte);
        var sitePassword = new StringBuilder(template.Length);
        for (var i = 0; i < template.Length; i++) {
            seedByte = _keyMaterial[i + 1];
            sitePassword.Append(SpectreType.GetClassCharacter(template[i], seedByte));
        }

        return sitePassword.ToString();
    }

    public override string ToString()
    {
        return ID;
    }
}