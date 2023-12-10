namespace Spectre.Services;

public interface IAlertService
{
    Task<bool> DisplayAlert(string title, string message, string confirm, string reject);
}
