// -----------------------------------------------------------------------
// <copyright file="MainPage.xaml.cs" company="Jim Borden">
// Copyright (c) Jim Borden. All rights reserved.
// Licensed under the GPL-3.0 license. See LICENSE.md file in the project root for full license information.
// </copyright>
// -----------------------------------------------------------------------

using Spectre.ViewModels;

namespace Spectre.Net;

/// <summary>
/// The main page of the app, where most of the interaction happens.
/// </summary>
public partial class MainPage : ContentPage
{
    /// <summary>
    /// Initializes a new instance of the <see cref="MainPage"/> class.
    /// </summary>
    /// <param name="vm">The view model to use as the binding context.</param>
    public MainPage(MainPageViewModel vm)
    {
        InitializeComponent();
        BindingContext = vm;
    }

    /// <inheritdoc />
    protected override void OnParentChanged()
    {
        base.OnParentChanged();
        _userPicker.SelectedIndex = 0;
    }
}