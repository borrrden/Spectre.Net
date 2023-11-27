using Microsoft.Maui.Controls;

using Spectre.ViewModels;

namespace Spectre.Net;

public partial class UserPreferencesPage : ContentPage
{
	public UserPreferencesPage(UserPreferencesViewModel vm)
	{
		InitializeComponent();
		BindingContext = vm;
	}
}