using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using CommunityToolkit.Mvvm.Messaging.Messages;

using Microsoft.Extensions.Logging;

using Spectre.Models;
using Spectre.Net.Api;
using Spectre.Services;

using System.Collections.Generic;
using System.Threading.Tasks;

namespace Spectre.ViewModels
{

    public sealed class SiteSettingsChangedMessage : ValueChangedMessage<string>
    {
        public SiteSettingsChangedMessage(string value) : base(value)
        {
        }
    }

    public readonly record struct SiteNameAndData(string siteName, SiteData siteData);

    public sealed class ApplySiteSettingsMessage : ValueChangedMessage<SiteNameAndData>
    {
        public ApplySiteSettingsMessage(SiteNameAndData value) : base(value)
        {
        }
    }

    public sealed partial class SiteSettingsViewModel : ObservableObject
    {
        private readonly ILogger<SiteSettingsViewModel> _logger = SpectreUtil.LoggerFactory.CreateLogger<SiteSettingsViewModel>();
        private readonly INavigationService _navigation;

        [ObservableProperty]
        private string _siteName = "test";

        [ObservableProperty]
        private PasswordType _selectedPasswordType = new PasswordType(SpectreResultType.Long);

        [ObservableProperty]
        private SpectreAlgorithmVersion _selectedAlgorithm;

        [ObservableProperty]
        private int _counterValue;
        private SiteData _siteData = new SiteData();

        public IList<SpectreAlgorithmVersion> AlgorithmChoices { get; } = new List<SpectreAlgorithmVersion> { SpectreAlgorithmVersion.v3 };

        public IList<PasswordType> PasswordTypeChoices { get; } = new List<PasswordType>
        {
            new PasswordType(SpectreResultType.Maximum),
            new PasswordType(SpectreResultType.Long),
            new PasswordType(SpectreResultType.Medium),
            new PasswordType(SpectreResultType.Short),
            new PasswordType(SpectreResultType.Basic),
            new PasswordType(SpectreResultType.PIN),
            new PasswordType(SpectreResultType.Name),
            new PasswordType(SpectreResultType.Phrase),
            new PasswordType(SpectreResultType.Personal),
            new PasswordType(SpectreResultType.Device),
            new PasswordType(SpectreResultType.DeriveKey)
        };

        public SiteSettingsViewModel(INavigationService navigation)
        {
            _navigation = navigation;
            WeakReferenceMessenger.Default.Register<SiteSettingsViewModel, ApplySiteSettingsMessage>(this, (r, e) => r.ApplySiteSettings(e.Value));
        }

        internal void ApplySiteSettings(SiteNameAndData siteNameAndData)
        {
            SiteName = siteNameAndData.siteName;
            var siteData = siteNameAndData.siteData;
            SelectedAlgorithm = (SpectreAlgorithmVersion)siteData.Algorithm;
            SelectedPasswordType = new PasswordType((SpectreResultType)siteData.Type);
            CounterValue = siteData.Counter;
            _siteData = siteData;
        }

        [RelayCommand]
        private async Task Save()
        {
            _siteData.Type = (int)SelectedPasswordType.ResultType;
            _siteData.Algorithm = (int)SelectedAlgorithm;
            _siteData.Counter = CounterValue;
            WeakReferenceMessenger.Default.Send(new SiteSettingsChangedMessage(SiteName));
            await _navigation.BackAsync();
        }
    }
}
