// -----------------------------------------------------------------------
// <copyright file="AddUserPage.xaml.cs" company="Jim Borden">
// Copyright (c) Jim Borden. All rights reserved.
// Licensed under the GPL-3.0 license. See LICENSE.md file in the project root for full license information.
// </copyright>
// -----------------------------------------------------------------------

using Spectre.ViewModels;

namespace Spectre.Net;

/// <summary>
/// The page used to add a new user to the app.
/// </summary>
public partial class AddUserPage : ContentPage
{
    /// <summary>
    /// Initializes a new instance of the <see cref="AddUserPage"/> class.
    /// </summary>
    /// <param name="vm">The view model to use as the binding context.</param>
    public AddUserPage(AddUserViewModel vm)
    {
        InitializeComponent();
        BindingContext = vm;
    }
}