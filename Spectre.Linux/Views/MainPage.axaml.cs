using Avalonia.Controls;
using Avalonia.Interactivity;

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
