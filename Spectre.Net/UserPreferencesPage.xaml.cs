// -----------------------------------------------------------------------
// <copyright file="UserPreferencesPage.xaml.cs" company="Jim Borden">
// Copyright (c) Jim Borden. All rights reserved.
// Licensed under the GPL-3.0 license. See LICENSE.md file in the project root for full license information.
// </copyright>
// -----------------------------------------------------------------------

using Spectre.ViewModels;

namespace Spectre.Net;

/// <summary>
/// The page the allows editing of user defaults / preferences.
/// </summary>
public partial class UserPreferencesPage : ContentPage
{
    /// <summary>
    /// Initializes a new instance of the <see cref="UserPreferencesPage"/> class.
    /// </summary>
    /// <param name="vm">The view model to use as the binding context.</param>
    public UserPreferencesPage(UserPreferencesViewModel vm)
    {
        InitializeComponent();
        BindingContext = vm;
    }
}