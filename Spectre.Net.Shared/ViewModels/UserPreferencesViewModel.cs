using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using CommunityToolkit.Mvvm.Messaging.Messages;

using Microsoft.Extensions.Logging;

using Spectre.Models;
using Spectre.Net.Api;
using Spectre.Services;

using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Spectre.ViewModels;

public sealed class SaveUserMessage : ValueChangedMessage<bool>
{
    public SaveUserMessage(bool value) : base(value)
    {
    }
}

public sealed class PasswordType
{
    private static readonly IReadOnlyDictionary<SpectreResultType, string> DisplayNameMapping = new Dictionary<SpectreResultType, string>
    {
        [SpectreResultType.Maximum] = "Maximum Security",
        [SpectreResultType.Long] = "Long Password",
        [SpectreResultType.Medium] = "Medium Password",
        [SpectreResultType.Short] = "Short Password",
        [SpectreResultType.Basic] = "Basic Password",
        [SpectreResultType.PIN] = "PIN Code",
        [SpectreResultType.Name] = "Name",
        [SpectreResultType.Phrase] = "Phrase",
        [SpectreResultType.Personal] = "Saved",
        [SpectreResultType.Device] = "Private",
        [SpectreResultType.DeriveKey] = "Binary Key"
    };

    public SpectreResultType ResultType { get; }

    public string DisplayName => DisplayNameMapping[ResultType];

    public PasswordType(SpectreResultType type)
    {
        ResultType = type;
    }

    public override int GetHashCode()
    {
        return ResultType.GetHashCode();
    }

    public override bool Equals(object? obj)
    {
        if (!(obj is PasswordType other))
        {
            return false;
        }

        return other.ResultType == ResultType;
    }
}

public sealed partial class UserPreferencesViewModel : ObservableObject
{
    [ObservableProperty]
    private string _userName = "";

    [ObservableProperty]
    private PasswordType _selectedPasswordType = new PasswordType(SpectreResultType.Long);

    [ObservableProperty]
    private SpectreAlgorithmVersion _selectedAlgorithm;

    [ObservableProperty]
    private bool _hiddenPasswords;

    private UserData _userData = new UserData();


    private readonly ILogger<UserPreferencesViewModel> _logger = SpectreUtil.LoggerFactory.CreateLogger<UserPreferencesViewModel>();
    private readonly INavigationService _navigation;

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

    public UserPreferencesViewModel(INavigationService navigation)
    {
        _navigation = navigation;
    }

    public void ApplyQueryAttributes(IDictionary<string, object> query)
    {
        if(!query.TryGetValue("userData", out var o) || o is not UserData userData) {
            _logger.LogError("Invalid userData received!");
            return;
        }

        UserName = userData.FullName;
        SelectedPasswordType = PasswordTypeChoices.First(x => (int)x.ResultType == userData.DefaultType);
        SelectedAlgorithm = (SpectreAlgorithmVersion)userData.Algorithm;
        HiddenPasswords = userData.HidePasswords;
        _userData = userData;
    }

    [RelayCommand]
    private async Task Save()
    {
        UserDefaults.Current.DefaultPasswordType = SelectedPasswordType.ResultType;
        UserDefaults.Current.AlgorithmVersion = SelectedAlgorithm;
        UserDefaults.Current.HiddenPasswords = HiddenPasswords;
        _userData.DefaultType = (int)SelectedPasswordType.ResultType;
        _userData.Algorithm = (int)SelectedAlgorithm;
        _userData.HidePasswords = HiddenPasswords;
        WeakReferenceMessenger.Default.Send(new SaveUserMessage(true));
        await _navigation.BackAsync();
    }
}
