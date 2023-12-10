// -----------------------------------------------------------------------
// <copyright file="AddUserViewModel.cs" company="Jim Borden">
// Copyright (c) Jim Borden. All rights reserved.
// Licensed under the GPL-3.0 license. See LICENSE.md file in the project root for full license information.
// </copyright>
// -----------------------------------------------------------------------

using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using Spectre.Models;
using Spectre.Services;

namespace Spectre.ViewModels;

/// <summary>
/// The view model for the add user page of the application.
/// </summary>
/// <param name="navigation">The object controlling page to page navigation.</param>
public sealed partial class AddUserViewModel(INavigationService navigation)
{
    private readonly INavigationService _navigation = navigation;

    /// <summary>
    /// Gets or sets a value indicating the username of the user being created.
    /// </summary>
    public string UserName { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets a value indicating whether the user should not be saved to disk.
    /// </summary>
    public bool Incognito { get; set; }

    [RelayCommand]
    private async Task Finish()
    {
        WeakReferenceMessenger.Default.Send(new NewUserMessage(new UserListEntry(UserName, Incognito)));
        await _navigation.BackAsync();
    }
}