// -----------------------------------------------------------------------
// <copyright file="ISpectreUserKey.cs" company="Jim Borden">
// Copyright (c) Jim Borden. All rights reserved.
// Licensed under the GPL-3.0 license. See LICENSE.md file in the project root for full license information.
// </copyright>
// -----------------------------------------------------------------------

namespace Spectre.Net.Api;

/// <summary>
/// An interface for an object that holds derived key data for a given user.
/// </summary>
public interface ISpectreUserKey
{
    /// <summary>
    /// Gets the unique ID of the key.
    /// </summary>
    string ID { get; }

    /// <summary>
    /// Gets the version used to generate the key.
    /// </summary>
    SpectreAlgorithmVersion Version { get; }

    /// <summary>
    /// Derive site key data based on the current user's key data mixed with the provided site data.
    /// </summary>
    /// <param name="siteName">The name of the site to derive the password for.</param>
    /// <param name="keyCounter">The counter to use when deriving the key.</param>
    /// <param name="keyPurpose">The purpose for deriving the key.</param>
    /// <param name="keyContext">Optional context to make an otherwise identical key unique.</param>
    /// <returns>The derived site key data.</returns>
    ISpectreSiteKey CreateSiteKey(string siteName, SpectreCounter keyCounter, SpectreKeyPurpose keyPurpose, string? keyContext);
}