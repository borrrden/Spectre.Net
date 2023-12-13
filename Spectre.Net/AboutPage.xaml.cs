// -----------------------------------------------------------------------
// <copyright file="AboutPage.xaml.cs" company="Jim Borden">
// Copyright (c) Jim Borden. All rights reserved.
// Licensed under the GPL-3.0 license. See LICENSE.md file in the project root for full license information.
// </copyright>
// -----------------------------------------------------------------------

using Spectre.ViewModels;

namespace Spectre.Net;

/// <summary>
/// The page showing information about the app.
/// </summary>
public partial class AboutPage : ContentPage
{
    /// <summary>
    /// Initializes a new instance of the <see cref="AboutPage"/> class.
    /// </summary>
    /// <param name="vm">The view model used for the binding context.</param>
    public AboutPage(AboutViewModel vm)
    {
        InitializeComponent();
        BindingContext = vm;
    }
}