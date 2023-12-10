using Spectre.ViewModels;

namespace Spectre.Net
{
    public partial class MainPage : ContentPage
    {
        public MainPage(MainPageViewModel vm)
        {
            InitializeComponent();
            BindingContext = vm;
        }

        protected override void OnParentChanged()
        {
            base.OnParentChanged();
            _userPicker.SelectedIndex = 0;
        }
    }

}
