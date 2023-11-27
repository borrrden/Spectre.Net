using Microsoft.Maui.Controls;

using System.Threading.Tasks;

namespace Spectre.Services
{
    internal sealed class AlertService : IAlertService
    {
        public Task<bool> DisplayAlert(string title, string message, string confirm, string reject)
            => Shell.Current.DisplayAlert(title, message, confirm, reject);
    }
}
