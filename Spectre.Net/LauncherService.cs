
/* Unmerged change from project 'Spectre.Net (net8.0-android)'
Before:
using Microsoft.Maui.ApplicationModel;

using System.Threading.Tasks;
After:
using System.Threading.Tasks;
using Microsoft.Maui.ApplicationModel;
*/

/* Unmerged change from project 'Spectre.Net (net8.0-windows10.0.19041.0)'
Before:
using Microsoft.Maui.ApplicationModel;

using System.Threading.Tasks;
After:
using System.Threading.Tasks;
using Microsoft.Maui.ApplicationModel;
*/

/* Unmerged change from project 'Spectre.Net (net8.0-maccatalyst)'
Before:
using Microsoft.Maui.ApplicationModel;

using System.Threading.Tasks;
After:
using System.Threading.Tasks;
using Microsoft.Maui.ApplicationModel;
*/
namespace Spectre.Services
{
    internal sealed class LauncherService : ILauncherService
    {
        public Task OpenAsync(string url) => Launcher.OpenAsync(url);
    }
}
