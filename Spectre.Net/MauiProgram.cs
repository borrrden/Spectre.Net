// -----------------------------------------------------------------------
// <copyright file="MauiProgram.cs" company="Jim Borden">
// Copyright (c) Jim Borden. All rights reserved.
// Licensed under the GPL-3.0 license. See LICENSE.md file in the project root for full license information.
// </copyright>
// -----------------------------------------------------------------------

using CommunityToolkit.Maui;
#if DEBUG
using Microsoft.Extensions.Logging;
#endif
using Spectre.Net.Api;
using Spectre.Services;
using Spectre.ViewModels;

namespace Spectre.Net;

/// <summary>
/// The definition of the MAUI program.
/// </summary>
public static class MauiProgram
{
    /// <summary>
    /// Standard function for creating the <see cref="MauiApp"/> used.
    /// </summary>
    /// <returns>The created MAUI app.</returns>
    public static MauiApp CreateMauiApp()
    {
        var builder = MauiApp.CreateBuilder();
        builder
            .UseMauiApp<App>()
            .UseMauiCommunityToolkit()
            .ConfigureFonts(fonts =>
            {
                fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
                fonts.AddFont("fa-free-400.otf", "FontAwesomeFree400");
                fonts.AddFont("fa-solid-900.otf", "FontAwesomeSolid900");
            });

#if DEBUG
        builder.Logging.AddDebug();
#endif

        builder.Services.AddSingleton<MainPage>();
        builder.Services.AddTransient<AboutPage>();
        builder.Services.AddTransient<AddUserPage>();
        builder.Services.AddTransient<UserPreferencesPage>();
        builder.Services.AddTransient<SiteSettingsPage>();

        builder.Services.AddTransient<AboutViewModel>();
        builder.Services.AddSingleton<MainPageViewModel>();
        builder.Services.AddTransient<AddUserViewModel>();
        builder.Services.AddTransient<UserPreferencesViewModel>();
        builder.Services.AddTransient<SiteSettingsViewModel>();

        builder.Services.AddSingleton<ISpectreUserKeyProvider, SpectreUserKeyProvider>();
        builder.Services.AddSingleton<ISpectreUserManager, SpectreUserManager>();
        builder.Services.AddSingleton<INavigationService, NavigationService>();
        builder.Services.AddSingleton<ILauncherService, LauncherService>();
        builder.Services.AddSingleton<IAlertService, AlertService>();

        return builder.Build();
    }
}