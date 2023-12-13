// -----------------------------------------------------------------------
// <copyright file="IAlertService.cs" company="Jim Borden">
// Copyright (c) Jim Borden. All rights reserved.
// Licensed under the GPL-3.0 license. See LICENSE.md file in the project root for full license information.
// </copyright>
// -----------------------------------------------------------------------

namespace Spectre.Services;

/// <summary>
/// An interface for an object which can display an alert (e.g. popup on mobile devices).
/// </summary>
public interface IAlertService
{
    /// <summary>
    /// Displays an alert with the given properties.
    /// </summary>
    /// <param name="title">The title of the displayed alert.</param>
    /// <param name="message">The message text displayed in the alert.</param>
    /// <param name="confirm">The "OK" button message.</param>
    /// <param name="reject">The "cancel" button message.</param>
    /// <returns>An awaitable task containing whether or not the user pressed "OK".</returns>
    Task<bool> DisplayAlert(string title, string message, string confirm, string reject);
}