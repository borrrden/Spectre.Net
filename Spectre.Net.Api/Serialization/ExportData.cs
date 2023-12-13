// -----------------------------------------------------------------------
// <copyright file="ExportData.cs" company="Jim Borden">
// Copyright (c) Jim Borden. All rights reserved.
// Licensed under the GPL-3.0 license. See LICENSE.md file in the project root for full license information.
// </copyright>
// -----------------------------------------------------------------------

namespace Spectre.Net.Api;

/// <summary>
/// Metadata about a Spectre disk save.
/// </summary>
public sealed class ExportData
{
    /// <summary>
    /// Gets the date and time that this save was created.
    /// </summary>
    public DateTimeOffset Date { get; init; } = default;

    /// <summary>
    /// Gets a value indicating whether or not this save file is redacted.
    /// </summary>
    public bool Redacted { get; init; } = true;

    /// <summary>
    /// Gets the format (version) of the save.
    /// </summary>
    public int Format { get; init; } = 0;
}