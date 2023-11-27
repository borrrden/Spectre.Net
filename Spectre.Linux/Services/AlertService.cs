using Spectre.Services;

using System;
using System.Threading.Tasks;

namespace Spectre.Linux.Services
{
    internal sealed class AlertService : IAlertService
    {
        public Task<bool> DisplayAlert(string title, string message, string confirm, string reject)
        {
            throw new NotImplementedException();
        }
    }
}
