
/* Unmerged change from project 'Spectre.Net (net8.0-android)'
Before:
using Microsoft.Maui.Controls;

using System.Collections.Generic;
using System.Threading.Tasks;
After:
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.Maui.Controls;
*/

/* Unmerged change from project 'Spectre.Net (net8.0-windows10.0.19041.0)'
Before:
using Microsoft.Maui.Controls;

using System.Collections.Generic;
using System.Threading.Tasks;
After:
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.Maui.Controls;
*/

/* Unmerged change from project 'Spectre.Net (net8.0-maccatalyst)'
Before:
using Microsoft.Maui.Controls;

using System.Collections.Generic;
using System.Threading.Tasks;
After:
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.Maui.Controls;
*/
namespace Spectre.Services
{
    internal sealed class NavigationService : INavigationService
    {
        public async Task BackAsync(IDictionary<string, object>? data = null)
        {
            await GoToAsync("..", data);
        }

        public async Task GoToAsync(string name, IDictionary<string, object>? data = null)
        {
            if (data == null) {
                await Shell.Current.GoToAsync(name);
            } else {
                await Shell.Current.GoToAsync(name, data);
            }
        }
    }
}
