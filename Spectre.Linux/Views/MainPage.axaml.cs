using Avalonia.Controls;
using Avalonia.Interactivity;

using Microsoft.Extensions.DependencyInjection;

using Spectre.ViewModels;

namespace Spectre.Linux.Views
{
    public partial class MainPage : UserControl
    {
        public MainPage()
        {
            InitializeComponent();
        }

        protected override void OnLoaded(RoutedEventArgs e)
        {
            base.OnLoaded(e);

            _userPicker.SelectedIndex = 0;
        }
    }
}
