// -----------------------------------------------------------------------
// <copyright file="MainWindowViewModel.cs" company="Jim Borden">
// Copyright (c) Jim Borden. All rights reserved.
// Licensed under the GPL-3.0 license. See LICENSE.md file in the project root for full license information.
// </copyright>
// -----------------------------------------------------------------------

using CommunityToolkit.Mvvm.ComponentModel;

using Microsoft.Extensions.DependencyInjection;

using Spectre.Linux;

namespace Spectre.ViewModels;

/// <summary>
/// Gets the view model for the main window of the Avalonia app.
/// </summary>
public partial class MainWindowViewModel : ObservableObject
{
    [ObservableProperty]
    private ObservableObject _contentViewModel = App.Services.GetRequiredService<MainPageViewModel>();
}