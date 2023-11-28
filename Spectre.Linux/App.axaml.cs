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

using System;

namespace Spectre.Linux;

public partial class App : Application
{
    public static IHost AppHost { get; }

    public static IServiceProvider Services => AppHost.Services;

    static App()
    {
        AppHost = Host.CreateDefaultBuilder().ConfigureServices(serviceCollection =>
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

    public override void Initialize()
    {
        AvaloniaXamlLoader.Load(this);
    }

    public override void OnFrameworkInitializationCompleted()
    {
        var mainWindowViewModel = new MainWindowViewModel();
        if (ApplicationLifetime is IClassicDesktopStyleApplicationLifetime desktop)
        {
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