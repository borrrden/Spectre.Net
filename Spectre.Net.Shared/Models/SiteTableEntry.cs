// -----------------------------------------------------------------------
// <copyright file="SiteTableEntry.cs" company="Jim Borden">
// Copyright (c) Jim Borden. All rights reserved.
// Licensed under the GPL-3.0 license. See LICENSE.md file in the project root for full license information.
// </copyright>
// -----------------------------------------------------------------------

using Spectre.Net.Api;

namespace Spectre.Models;

/// <summary>
/// Information for an entry in the list of sites in the app UI.
/// </summary>
public sealed class SiteTableEntry
{
    private readonly int _algorithm = 3;
    private readonly DateTimeOffset _lastUsed;

    /// <summary>
    /// Initializes a new instance of the <see cref="SiteTableEntry"/> class.
    /// </summary>
    /// <param name="spectreSave">The save data to use for reading existing values and writing new ones.</param>
    /// <param name="siteName">The name of the site to create or edit.</param>
    public SiteTableEntry(SpectreSave spectreSave, string siteName)
    {
        SiteName = siteName;
        Exists = spectreSave.Sites.TryGetValue(siteName, out var site);
        if (Exists) {
            _algorithm = site!.Algorithm;
            _lastUsed = site.LastUsed;
            Counter = site.Counter;
            ResultType = (SpectreResultType)site.Type;
        }
    }

    /// <summary>
    /// Gets the name of the site (e.g. duckduckgo.com).
    /// </summary>
    public string SiteName { get; }

    /// <summary>
    /// Gets the counter to use when deriving the password.
    /// </summary>
    public int Counter { get; } = 1;

    /// <summary>
    /// Gets the type of password to derive.
    /// </summary>
    public SpectreResultType ResultType { get; } = SpectreResultType.Long;

    /// <summary>
    /// Gets a value indicating whether or not this site has been saved.
    /// </summary>
    public bool Exists { get; }

    /// <inheritdoc />
    public override string ToString()
    {
        return Exists ? $"{SiteName} ({_lastUsed.ToLocalTime():MMM dd, yyyy, hh:mm:ss tt} - V{_algorithm} - #{Counter})"
            : $"{SiteName} <Add new site>";
    }
}