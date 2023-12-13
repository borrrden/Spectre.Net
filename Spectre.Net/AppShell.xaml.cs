// -----------------------------------------------------------------------
// <copyright file="AppShell.xaml.cs" company="Jim Borden">
// Copyright (c) Jim Borden. All rights reserved.
// Licensed under the GPL-3.0 license. See LICENSE.md file in the project root for full license information.
// </copyright>
// -----------------------------------------------------------------------

namespace Spectre.Net;

/// <summary>
/// The top level navigation component of the MAUI app.
/// </summary>
public partial class AppShell : Shell
{
    /// <summary>
    /// Initializes a new instance of the <see cref="AppShell"/> class.
    /// </summary>
    public AppShell()
    {
        InitializeComponent();

        Routing.RegisterRoute("about", typeof(AboutPage));
        Routing.RegisterRoute("addUser", typeof(AddUserPage));
        Routing.RegisterRoute("userPrefs", typeof(UserPreferencesPage));
        Routing.RegisterRoute("siteSettings", typeof(SiteSettingsPage));
    }
}