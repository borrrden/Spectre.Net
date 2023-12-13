// -----------------------------------------------------------------------
// <copyright file="UserData.cs" company="Jim Borden">
// Copyright (c) Jim Borden. All rights reserved.
// Licensed under the GPL-3.0 license. See LICENSE.md file in the project root for full license information.
// </copyright>
// -----------------------------------------------------------------------

namespace Spectre.Net.Api;

/// <summary>
/// Information about a user in a Spectre save.
/// </summary>
public sealed class UserData
{
    /// <summary>
    /// Gets a value indicating which avatar to use (unused by this app).
    /// </summary>
    public int Avatar { get; init; } = 0;

    /// <summary>
    /// Gets or sets the full name of the user.
    /// </summary>
    public string FullName { get; set; } = string.Empty;

    /// <summary>
    /// Gets the identicon representation of the user's master password (unused by this app).
    /// </summary>
    public string Identicon { get; init; } = string.Empty;

    /// <summary>
    /// Gets or sets a value indicating whether or not to mask derived passwords in the UI for this user.
    /// </summary>
    public bool HidePasswords { get; set; } = false;

    /// <summary>
    /// Gets or sets the default derivation algorithm for this user.
    /// </summary>
    public int Algorithm { get; set; } = 0;

    /// <summary>
    /// Gets or sets the ID of this user's derived key, so that the user's input master password
    /// can be validated.
    /// </summary>
    public string KeyID { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the default type of password to generate.
    /// </summary>
    public int DefaultType { get; set; } = 0;

    /// <summary>
    /// Gets the default type of login to generate.
    /// </summary>
    public int LoginType { get; init; } = 0;

    /// <summary>
    /// Gets or sets the last time this user was used in the app.
    /// </summary>
    public DateTimeOffset LastUsed { get; set; } = default;
}