using Microsoft.Maui.ApplicationModel;

using System.Threading.Tasks;

namespace Spectre.Services
{
    internal sealed class LauncherService : ILauncherService
    {
        public Task OpenAsync(string url) => Launcher.OpenAsync(url);
    }
}
