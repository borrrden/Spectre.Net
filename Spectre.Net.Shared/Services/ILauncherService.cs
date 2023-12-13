// -----------------------------------------------------------------------
// <copyright file="ILauncherService.cs" company="Jim Borden">
// Copyright (c) Jim Borden. All rights reserved.
// Licensed under the GPL-3.0 license. See LICENSE.md file in the project root for full license information.
// </copyright>
// -----------------------------------------------------------------------

namespace Spectre.Services;

/// <summary>
/// An interface for an object that can open and display the contents of a URL.
/// </summary>
public interface ILauncherService
{
    /// <summary>
    /// Opens the given URL (probably in a web browser of some sort).
    /// </summary>
    /// <param name="url">The URL to open.</param>
    /// <returns>An awaitable task that represents the progress of the display.</returns>
    Task OpenAsync(string url);
}