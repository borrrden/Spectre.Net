using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

using Spectre.Services;

using System.Threading.Tasks;

namespace Spectre.ViewModels
{
    public abstract partial class BaseViewModel : ObservableObject
    {
        private readonly INavigationService _navigationService;

        public BaseViewModel(INavigationService navigationService)
        {
            _navigationService = navigationService;
        }

        [RelayCommand]
        private Task Back()
        {
            return _navigationService.BackAsync();
        }
    }
}
