using Microsoft.Maui.Controls;

using Spectre.ViewModels;

namespace Spectre.Net;

public partial class SiteSettingsPage : ContentPage
{
	public SiteSettingsPage(SiteSettingsViewModel vm)
	{
		InitializeComponent();
		BindingContext = vm;
	}
}