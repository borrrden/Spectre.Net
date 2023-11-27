using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

using Spectre.Services;

using System;
using System.IO;
using System.Threading.Tasks;

namespace Spectre.ViewModels
{
    public sealed partial class AboutViewModel : BaseViewModel
    {
        private readonly ILauncherService _launcher;

        public string PersistenceLocation => Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.UserProfile), ".spectre.d");

        public AboutViewModel(INavigationService navigationService, ILauncherService launcher)
            : base(navigationService)
        {
            _launcher = launcher;
        }

        [RelayCommand]
        private async Task OpenWebsite(string url)
        {
            await _launcher.OpenAsync(url);
        }
    }
}
