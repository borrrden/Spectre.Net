namespace Spectre.Services;

public interface ILauncherService
{
    Task OpenAsync(string url);
}
