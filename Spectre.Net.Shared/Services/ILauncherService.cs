using System.Threading.Tasks;

namespace Spectre.Services;

public interface ILauncherService
{
    Task OpenAsync(string url);
}
