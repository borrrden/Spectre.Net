// -----------------------------------------------------------------------
// <copyright file="SpectreUserKey_v3.cs" company="Jim Borden">
// Copyright (c) Jim Borden. All rights reserved.
// Licensed under the GPL-3.0 license. See LICENSE.md file in the project root for full license information.
// </copyright>
// -----------------------------------------------------------------------

using System.Security.Cryptography;
using System.Text;
using Microsoft.Extensions.Logging;

namespace Spectre.Net.Api.Algorithms;

internal sealed class SpectreUserKey_v3 : ISpectreUserKey
{
    private const int ScryptN = 32768;
    private const int ScryptR = 8;
    private const int ScryptP = 2;
    private const int ScryptDKLen = 64;
    private readonly ILogger<SpectreUserKey_v3> _logger = SpectreUtil.LoggerFactory.CreateLogger<SpectreUserKey_v3>();

    private readonly byte[] _keyMaterial;

    public SpectreUserKey_v3(string username, string userSecret)
    {
        var scope = SpectreKeyPurpose.Authentication.ToPurposeScope();
        _logger.LogTrace("keyScope: {scope}", scope);
        _logger.LogTrace("userKeySalt: keyScope={keyScope} | #userName={userNameLength:X9} | userName={username}", scope, username.Length, username);
        using var userKeySalt = new MemoryStream();
        using var writer = new BinaryWriter(userKeySalt, Encoding.ASCII);
        userKeySalt.Write(Encoding.ASCII.GetBytes(scope));
        userKeySalt.Write(BitConverter.GetBytes(username.Length).Reverse().ToArray());
        userKeySalt.Write(Encoding.ASCII.GetBytes(username));
        var buffer = userKeySalt.ToArray();
        _logger.LogTrace(" => userKeySalt.id: {userKeySaltID}", Convert.ToHexString(SHA256.HashData(buffer)));
        _logger.LogTrace("userKey: scrypt(userSecret, userKeySalt, N={N}, R={R}, P={P})", ScryptN, ScryptR, ScryptP);
        _keyMaterial = new byte[ScryptDKLen];
        var scryptResult = Interop.crypto_pwhash_scryptsalsa208sha256_ll(
            Encoding.ASCII.GetBytes(userSecret),
            (nuint)userSecret.Length,
            buffer,
            (nuint)buffer.Length,
            ScryptN,
            ScryptR,
            ScryptP,
            _keyMaterial,
            (nuint)_keyMaterial.Length);

        if (scryptResult != 0) {
            var e = new ApplicationException($"Failed to scrypt ({scryptResult})");
            _logger.LogError(e, "Failed to scrypt!");
            throw e;
        }

        ID = SpectreUtil.StringSHA256(_keyMaterial);
        _logger.LogTrace(" => userKey.id: {userKeyID}", this);
    }

    public string ID { get; }

    public SpectreAlgorithmVersion Version => SpectreAlgorithmVersion.V3;

    public ISpectreSiteKey CreateSiteKey(string siteName, SpectreCounter keyCounter, SpectreKeyPurpose keyPurpose, string? keyContext)
        => new SpectreSiteKey_v3(_keyMaterial, siteName, keyCounter, keyPurpose, keyContext);

    public override int GetHashCode()
    {
        return ID.GetHashCode();
    }

    public override string ToString()
    {
        return ID;
    }
}