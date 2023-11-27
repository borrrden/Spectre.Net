using Microsoft.Maui.Controls;

namespace Spectre.Net
{
    public partial class AppShell : Shell
    {
        public AppShell()
        {
            InitializeComponent();

            Routing.RegisterRoute("about", typeof(AboutPage));
            Routing.RegisterRoute("addUser", typeof(AddUserPage));
            Routing.RegisterRoute("userPrefs", typeof(UserPreferencesPage));
            Routing.RegisterRoute("siteSettings", typeof(SiteSettingsPage));
        }
    }
}
