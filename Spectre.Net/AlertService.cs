
/* Unmerged change from project 'Spectre.Net (net8.0-android)'
Before:
using Microsoft.Maui.Controls;

using System.Threading.Tasks;
After:
using System.Threading.Tasks;
using Microsoft.Maui.Controls;
*/

/* Unmerged change from project 'Spectre.Net (net8.0-windows10.0.19041.0)'
Before:
using Microsoft.Maui.Controls;

using System.Threading.Tasks;
After:
using System.Threading.Tasks;
using Microsoft.Maui.Controls;
*/

/* Unmerged change from project 'Spectre.Net (net8.0-maccatalyst)'
Before:
using Microsoft.Maui.Controls;

using System.Threading.Tasks;
After:
using System.Threading.Tasks;
using Microsoft.Maui.Controls;
*/
namespace Spectre.Services
{
    internal sealed class AlertService : IAlertService
    {
        public Task<bool> DisplayAlert(string title, string message, string confirm, string reject)
            => Shell.Current.DisplayAlert(title, message, confirm, reject);
    }
}
