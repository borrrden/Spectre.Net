using CommunityToolkit.Mvvm.ComponentModel;

using Microsoft.Extensions.DependencyInjection;

using Spectre.Linux;

namespace Spectre.ViewModels;

public partial class MainWindowViewModel : ObservableObject
{
    [ObservableProperty]
    private ObservableObject _contentViewModel = App.Services.GetRequiredService<MainPageViewModel>();
}
