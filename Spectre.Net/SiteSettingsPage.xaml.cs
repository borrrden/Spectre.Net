// -----------------------------------------------------------------------
// <copyright file="SiteSettingsPage.xaml.cs" company="Jim Borden">
// Copyright (c) Jim Borden. All rights reserved.
// Licensed under the GPL-3.0 license. See LICENSE.md file in the project root for full license information.
// </copyright>
// -----------------------------------------------------------------------

using Spectre.ViewModels;

namespace Spectre.Net;

/// <summary>
/// The page for editing settings for a given site.
/// </summary>
public partial class SiteSettingsPage : ContentPage
{
    /// <summary>
    /// Initializes a new instance of the <see cref="SiteSettingsPage"/> class.
    /// </summary>
    /// <param name="vm">The view model to use as the binding context.</param>
    public SiteSettingsPage(SiteSettingsViewModel vm)
    {
        InitializeComponent();
        BindingContext = vm;
    }
}