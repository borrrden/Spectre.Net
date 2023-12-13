// -----------------------------------------------------------------------
// <copyright file="SpectreSave.cs" company="Jim Borden">
// Copyright (c) Jim Borden. All rights reserved.
// Licensed under the GPL-3.0 license. See LICENSE.md file in the project root for full license information.
// </copyright>
// -----------------------------------------------------------------------

namespace Spectre.Net.Api;

/// <summary>
/// A serialization class for user data saved to disk.
/// </summary>
public sealed class SpectreSave
{
    /// <summary>
    /// Gets the information about how this save was exported.
    /// </summary>
    public ExportData Export { get; init; } = new ExportData();

    /// <summary>
    /// Gets the user data for this save.
    /// </summary>
    public UserData User { get; init; } = new UserData();

    /// <summary>
    /// Gets the list of known sites for this save.
    /// </summary>
    public IReadOnlyDictionary<string, SiteData> Sites { get; init; } = new Dictionary<string, SiteData>();
}