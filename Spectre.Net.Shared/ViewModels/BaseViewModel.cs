// -----------------------------------------------------------------------
// <copyright file="BaseViewModel.cs" company="Jim Borden">
// Copyright (c) Jim Borden. All rights reserved.
// Licensed under the GPL-3.0 license. See LICENSE.md file in the project root for full license information.
// </copyright>
// -----------------------------------------------------------------------

using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Spectre.Services;

namespace Spectre.ViewModels;

/// <summary>
/// The base class for view models in the app.
/// </summary>
/// <param name="navigationService">The service that can perform page to page navigation.</param>
public abstract partial class BaseViewModel(INavigationService navigationService) : ObservableObject
{
    private readonly INavigationService _navigationService = navigationService;

    [RelayCommand]
    private Task Back()
    {
        return _navigationService.BackAsync();
    }
}