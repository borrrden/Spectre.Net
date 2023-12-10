// -----------------------------------------------------------------------
// <copyright file="MainPageViewModel.cs" company="Jim Borden">
// Copyright (c) Jim Borden. All rights reserved.
// Licensed under the GPL-3.0 license. See LICENSE.md file in the project root for full license information.
// </copyright>
// -----------------------------------------------------------------------

using System.Collections.ObjectModel;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using Spectre.Models;
using Spectre.Net.Api;
using Spectre.Services;

namespace Spectre.ViewModels;

/// <summary>
/// The view model for the main page of the application.
/// </summary>
public sealed partial class MainPageViewModel : ObservableObject
{
    private readonly ISpectreUserKeyProvider _userKeyProvider;
    private readonly ISpectreUserManager _userManager;
    private readonly INavigationService _navigation;
    private readonly IAlertService _alertService;

    [ObservableProperty]
    private ObservableCollection<UserListEntry> _userList = [];

    [ObservableProperty]
    private UserListEntry _selectedUser = new(string.Empty, true);

    [ObservableProperty]
    [NotifyPropertyChangedFor(nameof(SelectedSite))]
    [NotifyPropertyChangedFor(nameof(GeneratedPassword))]
    private SiteTableEntry? _selectedItem = null;

    [ObservableProperty]
    private string? _siteEntryText;

    [ObservableProperty]
    private ObservableCollection<SiteTableEntry> _savedSites = [];

    [ObservableProperty]
    private bool _loggedIn = false;

    [ObservableProperty]
    private string _password = string.Empty;

    [ObservableProperty]
    private string _errorText = string.Empty;

    private SpectreSave? _spectreSave;
    private ISpectreUserKey? _spectreUserKey;

    /// <summary>
    /// Initializes a new instance of the <see cref="MainPageViewModel"/> class.
    /// </summary>
    /// <remarks>This is only used by the XAML designer.</remarks>
#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
    public MainPageViewModel()
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="MainPageViewModel"/> class.
    /// </summary>
    /// <param name="userKeyProvider">The object that can create a Spectre user key derivation.</param>
    /// <param name="userManager">The object that can manage user persistence to disk.</param>
    /// <param name="navigation">The object that handles page to page navigation.</param>
    /// <param name="alertService">The object that can show graphical alerts.</param>
    public MainPageViewModel(
        ISpectreUserKeyProvider userKeyProvider,
        ISpectreUserManager userManager,
        INavigationService navigation,
        IAlertService alertService)
    {
        _userKeyProvider = userKeyProvider;
        _userManager = userManager;
        _navigation = navigation;
        _alertService = alertService;
        GetUserList();
        UserDefaults.Current.PropertyChanged += (sender, args) =>
        {
            if (args.PropertyName == nameof(UserDefaults.HiddenPasswords)) {
                OnPropertyChanged(nameof(GeneratedPassword));
            }
        };

        WeakReferenceMessenger.Default.Register<MainPageViewModel, SiteSettingsChangedMessage>(this, (r, e) => r.SiteSettingsChanged(e.Value));
        WeakReferenceMessenger.Default.Register<MainPageViewModel, NewUserMessage>(this, (r, e) => r.NewUserAdded(e.Value));
        WeakReferenceMessenger.Default.Register<MainPageViewModel, SaveUserMessage>(this, (r, e) => r._userManager.UpdateUser(r.SelectedUser.UserName, r._spectreSave!));
    }

    /// <summary>
    /// Gets a value indicating the password generated from the user input.
    /// </summary>
    public string GeneratedPassword
    {
        get
        {
            var password = GeneratePassword(SelectedSite);
            return UserDefaults.Current.HiddenPasswords ? new string('*', password.Length) : password;
        }
    }

    /// <summary>
    /// Gets the currently selected site in the UI.
    /// </summary>
    public string SelectedSite => SelectedItem?.SiteName ?? string.Empty;

    internal void SiteSettingsChanged(string siteName)
    {
        var index = 0;
        foreach (var site in SavedSites) {
            if (site.SiteName == siteName) {
                break;
            }

            index++;
        }

        if (index < SavedSites.Count) {
            SavedSites.RemoveAt(index);
            SavedSites.Insert(index, new SiteTableEntry(_spectreSave!, siteName));
            _userManager.UpdateUser(SelectedUser.UserName, _spectreSave!);
            OnPropertyChanged(nameof(GeneratedPassword));
        }
    }

    internal void NewUserAdded(UserListEntry newUser)
    {
        UserList.Add(newUser);
        SelectedUser = UserList.Last();
    }

    [RelayCommand]
    private void Logout()
    {
        LoggedIn = false;
        _spectreUserKey = null;
    }

    [RelayCommand]
    private async Task Navigate(string destination)
    {
        await _navigation.GoToAsync(destination);
        switch (destination) {
            case "userPrefs":
                break;
            case "siteSettings":
                WeakReferenceMessenger.Default.Send(new ApplySiteSettingsMessage(new SiteNameAndData(SelectedSite, _spectreSave!.Sites[SelectedSite])));
                break;
            default:
                break;
        }
    }

    private string GeneratePassword(string siteName)
    {
        if (_spectreUserKey == null) {
            return string.Empty;
        }

        var siteEntry = SavedSites.FirstOrDefault(x => x.SiteName == siteName);
        var counter = siteEntry?.Counter ?? 1;
        var type = siteEntry?.ResultType ?? SpectreResultType.DefaultResult;
        var siteKey = _spectreUserKey.CreateSiteKey(siteName, (SpectreCounter)counter, SpectreKeyPurpose.Authentication, null);
        return siteKey.CreatePassword(type);
    }

    private void GetUserList()
    {
        foreach (var user in _userManager.AllUserNames()) {
            UserList.Add(new UserListEntry(user, false));
        }
    }

    private void GetSavedSites(string? filter = null)
    {
        if (_spectreSave == null) {
            return;
        }

        var exactMatchFound = false;
        var newSaved = new ObservableCollection<SiteTableEntry>(_spectreSave.Sites
            .Where(x =>
            {
                if (string.IsNullOrWhiteSpace(filter)) {
                    exactMatchFound = true;
                    return true;
                }

                if (!exactMatchFound) {
                    exactMatchFound = x.Key == filter;
                }

                return x.Key.Contains(filter);
            })
            .Select(x => new SiteTableEntry(_spectreSave, x.Key)));
        SavedSites = newSaved;
        if (!exactMatchFound && filter != null) {
            var newSite = new SiteTableEntry(_spectreSave, filter);
            SavedSites.Add(newSite);
        }

        SelectedItem = SavedSites.FirstOrDefault();
    }

    [RelayCommand]
    private void PasswordEntered(string pw)
    {
        _spectreUserKey = _userKeyProvider.CreateUserKey(SelectedUser.UserName, pw, SpectreAlgorithmVersion.V3);
        _spectreSave = _userManager.ReadUser(SelectedUser.UserName) ?? _userManager.CreateUser(SelectedUser.UserName, _spectreUserKey);

        if (_spectreSave.User.KeyID != _spectreUserKey.ID) {
            ErrorText = $"Incorrect master password for user: {SelectedUser}";
            _spectreUserKey = null;
            return;
        }

        UserDefaults.Current.ReadDefaults(_spectreSave.User);
        GetSavedSites();
        ErrorText = string.Empty;
        LoggedIn = true;
        Password = string.Empty;
    }

    [RelayCommand]
    private async Task DeleteUser()
    {
        var result = await _alertService.DisplayAlert("Delete User", $"Delete the user {SelectedUser}?\n\n{SelectedUser}.mpjson", "Yes", "No");
        if (result) {
            _userManager.DeleteUser(SelectedUser.UserName);
            UserList.Remove(SelectedUser);
        }
    }

    partial void OnSiteEntryTextChanged(string? value)
    {
        GetSavedSites(value);
    }
}