// -----------------------------------------------------------------------
// <copyright file="AboutViewModel.cs" company="Jim Borden">
// Copyright (c) Jim Borden. All rights reserved.
// Licensed under the GPL-3.0 license. See LICENSE.md file in the project root for full license information.
// </copyright>
// -----------------------------------------------------------------------

using CommunityToolkit.Mvvm.Input;
using Spectre.Services;

namespace Spectre.ViewModels;

/// <summary>
/// The view model for the about page of the application.
/// </summary>
/// <param name="navigationService">The object controlling the page to page navigation.</param>
/// <param name="launcher">The object that can open URLs in a browser.</param>
public sealed partial class AboutViewModel(INavigationService navigationService, ILauncherService launcher) : BaseViewModel(navigationService)
{
    private readonly ILauncherService _launcher = launcher;

    /// <summary>
    /// Gets the location of the saved user data on disk.
    /// </summary>
    public static string PersistenceLocation => Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.UserProfile), ".spectre.d");

    [RelayCommand]
    private async Task OpenWebsite(string url)
    {
        await _launcher.OpenAsync(url);
    }
}