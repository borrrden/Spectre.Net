using System.Collections.Generic;
using System.Threading.Tasks;

namespace Spectre.Services
{
    public interface INavigationService
    {
        Task GoToAsync(string name, IDictionary<string, object>? data = null);

        Task BackAsync(IDictionary<string, object>? data = null);
    }
}
