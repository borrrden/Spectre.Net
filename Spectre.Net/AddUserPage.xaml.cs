using Spectre.ViewModels;

namespace Spectre.Net;

public partial class AddUserPage : ContentPage
{
    public AddUserPage(AddUserViewModel vm)
    {
        InitializeComponent();
        BindingContext = vm;
    }


}