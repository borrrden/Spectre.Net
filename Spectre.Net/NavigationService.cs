using Microsoft.Maui.Controls;

using System.Collections.Generic;
using System.Threading.Tasks;

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
            if (data == null)
            {
                await Shell.Current.GoToAsync(name);
            }
            else
            {
                await Shell.Current.GoToAsync(name, data);
            }
        }
    }
}
