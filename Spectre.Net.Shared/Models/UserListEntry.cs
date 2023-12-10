// -----------------------------------------------------------------------
// <copyright file="UserListEntry.cs" company="Jim Borden">
// Copyright (c) Jim Borden. All rights reserved.
// Licensed under the GPL-3.0 license. See LICENSE.md file in the project root for full license information.
// </copyright>
// -----------------------------------------------------------------------

namespace Spectre.Models;

/// <summary>
/// An entry in the list of users for the application.
/// </summary>
/// <param name="userName">The username for the user.</param>
/// <param name="transient">Whether or not the user is persisted to disk.</param>
public sealed class UserListEntry(string userName, bool transient)
{
    /// <summary>
    /// Gets a value indicating the username of the user.
    /// </summary>
    public string UserName { get; } = userName;

    /// <summary>
    /// Gets a value indicating whether or not the user is persistent to disk.
    /// </summary>
    public bool Transient { get; } = transient;

    /// <inheritdoc />
    public override string ToString()
    {
        return UserName;
    }
}