// -----------------------------------------------------------------------
// <copyright file="INavigationService.cs" company="Jim Borden">
// Copyright (c) Jim Borden. All rights reserved.
// Licensed under the GPL-3.0 license. See LICENSE.md file in the project root for full license information.
// </copyright>
// -----------------------------------------------------------------------

namespace Spectre.Services;

/// <summary>
/// An interface for an object that can perform page to page navigation in a GUI app.
/// </summary>
public interface INavigationService
{
    /// <summary>
    /// Opens the page with the provided name and passes in optional data.
    /// </summary>
    /// <param name="name">The name of the page to open.</param>
    /// <param name="data">The data to pass to the page.</param>
    /// <returns>An awaitable task.</returns>
    Task GoToAsync(string name, IDictionary<string, object>? data = null);

    /// <summary>
    /// Opens the previous page and passes in optional data.
    /// </summary>
    /// <param name="data">The data to pass to the page.</param>
    /// <returns>An awaitable task.</returns>
    Task BackAsync(IDictionary<string, object>? data = null);
}