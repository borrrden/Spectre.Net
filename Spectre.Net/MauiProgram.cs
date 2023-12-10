using CommunityToolkit.Maui;
using Microsoft.Extensions.Logging;

using Spectre.Net.Api;
using Spectre.Services;
using Spectre.ViewModels;

namespace Spectre.Net
{
    public static class MauiProgram
    {
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
}
