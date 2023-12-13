// -----------------------------------------------------------------------
// <copyright file="SiteData.cs" company="Jim Borden">
// Copyright (c) Jim Borden. All rights reserved.
// Licensed under the GPL-3.0 license. See LICENSE.md file in the project root for full license information.
// </copyright>
// -----------------------------------------------------------------------

namespace Spectre.Net.Api;

/// <summary>
/// Information about site to derive a password for.
/// </summary>
public sealed class SiteData
{
    /// <summary>
    /// Gets or sets the counter to use when deriving the password.
    /// </summary>
    public int Counter { get; set; } = 0;

    /// <summary>
    /// Gets or sets the algorithm to use when deriving the password.
    /// </summary>
    public int Algorithm { get; set; } = 0;

    /// <summary>
    /// Gets or sets the type of password to derive.
    /// </summary>
    public int Type { get; set; } = 0;

    /// <summary>
    /// Gets the type of login to derive.
    /// </summary>
    public int LoginType { get; init; } = 0;

    /// <summary>
    /// Gets or sets the number of times this site has been used.
    /// </summary>
    public int Uses { get; set; } = 0;

    /// <summary>
    /// Gets or sets the last time this site was used.
    /// </summary>
    public DateTimeOffset LastUsed { get; set; } = default;
}