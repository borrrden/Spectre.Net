// -----------------------------------------------------------------------
// <copyright file="SpectreAlgorithm.cs" company="Jim Borden">
// Copyright (c) Jim Borden. All rights reserved.
// Licensed under the GPL-3.0 license. See LICENSE.md file in the project root for full license information.
// </copyright>
// -----------------------------------------------------------------------

using Microsoft.Extensions.Logging;

using Spectre.Net.Api.Algorithms;

namespace Spectre.Net.Api;

/// <summary>
/// The entry point into starting the process of user key derivation.
/// </summary>
public static class SpectreAlgorithm
{
    private static readonly ILogger _logger = SpectreUtil.LoggerFactory.CreateLogger(nameof(SpectreAlgorithm));

    /// <summary>
    /// Creates an instantiation of <see cref="ISpectreUserKey"/> that represents the derived key data
    /// for a given user.
    /// </summary>
    /// <param name="username">The username of the user.</param>
    /// <param name="userSecret">The user's secret password.</param>
    /// <param name="version">The version of the algortihm to use for dervation.</param>
    /// <returns>An <see cref="ISpectreUserKey"/> instance containing the derived user key data.</returns>
    /// <remarks>Only <see cref="SpectreAlgorithmVersion.V3"/> is supported.</remarks>
    public static ISpectreUserKey CreateUserKey(string username, string userSecret, SpectreAlgorithmVersion version)
    {
        ArgumentNullException.ThrowIfNull(username);
        ArgumentNullException.ThrowIfNull(userSecret);

        _logger.LogTrace("-- UserKey (algorithm: {version})", version);
        _logger.LogTrace("username: {username}", username);
        _logger.LogTrace("userSecret: {userSecret}", SpectreUtil.StringSHA256(userSecret));
        switch (version) {
            case SpectreAlgorithmVersion.V3:
                return new SpectreUserKey_v3(username, userSecret);
            default:
                var e = new NotSupportedException($"Unsupported algorithm: {version}");
                _logger.LogError(e, "Unsupported algorithm");
                throw e;
        }
    }
}