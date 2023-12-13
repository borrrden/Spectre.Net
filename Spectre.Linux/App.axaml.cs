// -----------------------------------------------------------------------
// <copyright file="App.axaml.cs" company="Jim Borden">
// Copyright (c) Jim Borden. All rights reserved.
// Licensed under the GPL-3.0 license. See LICENSE.md file in the project root for full license information.
// </copyright>
// -----------------------------------------------------------------------

using Avalonia;
using Avalonia.Controls.ApplicationLifetimes;
using Avalonia.Markup.Xaml;

using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

using Spectre.Linux.Services;
using Spectre.Linux.Views;
using Spectre.Net.Api;
using Spectre.Services;
using Spectre.ViewModels;

namespace Spectre.Linux;

/// <summary>
/// The top level object for the Avalonia application.
/// </summary>
public partial class App : Application
{
    private static readonly IHost _appHost;

    static App()
    {
        _appHost = Host.CreateDefaultBuilder().ConfigureServices(serviceCollection =>
        {
            serviceCollection.AddTransient<AboutViewModel>();
            serviceCollection.AddSingleton<MainPageViewModel>();
            serviceCollection.AddTransient<AddUserViewModel>();
            serviceCollection.AddTransient<UserPreferencesViewModel>();
            serviceCollection.AddTransient<SiteSettingsViewModel>();

            serviceCollection.AddSingleton<ISpectreUserKeyProvider, SpectreUserKeyProvider>();
            serviceCollection.AddSingleton<ISpectreUserManager, SpectreUserManager>();
            serviceCollection.AddSingleton<INavigationService, NavigationService>();
            serviceCollection.AddSingleton<ILauncherService, LauncherService>();
            serviceCollection.AddSingleton<IAlertService, AlertService>();
        }).Build();
    }

    /// <summary>
    /// Gets the provider for services for the application (to look up dependency injection classes).
    /// </summary>
    public static IServiceProvider Services => _appHost.Services;

    /// <inheritdoc />
    public override void Initialize()
    {
        AvaloniaXamlLoader.Load(this);
    }

    /// <inheritdoc />
    public override void OnFrameworkInitializationCompleted()
    {
        var mainWindowViewModel = new MainWindowViewModel();
        if (ApplicationLifetime is IClassicDesktopStyleApplicationLifetime desktop) {
            desktop.MainWindow = new MainWindow
            {
                DataContext = mainWindowViewModel
            };
        }

        (Services.GetRequiredService<INavigationService>() as NavigationService)!.RootViewModel = mainWindowViewModel;

        NavigationService.RegisterRoute<AboutViewModel>("about");

        base.OnFrameworkInitializationCompleted();
    }
}