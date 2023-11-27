using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using CommunityToolkit.Mvvm.Messaging.Messages;

using Spectre.Models;
using Spectre.Services;

using System.Collections.Generic;
using System.Threading.Tasks;

namespace Spectre.ViewModels
{
    public sealed class NewUserMessage : ValueChangedMessage<UserListEntry>
    {
        public NewUserMessage(UserListEntry value) : base(value)
        {
        }
    }

    public sealed partial class AddUserViewModel
    {
        private readonly INavigationService _navigation;

        public string UserName { get; set; } = "";

        public bool Incognito { get; set; }

        public AddUserViewModel(INavigationService navigation)
        {
            _navigation = navigation;
        }

        [RelayCommand]
        private async Task Finish()
        {
            WeakReferenceMessenger.Default.Send(new NewUserMessage(new UserListEntry(UserName, Incognito)));
            await _navigation.BackAsync();
        }
    }
}
