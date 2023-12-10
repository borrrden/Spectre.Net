// -----------------------------------------------------------------------
// <copyright file="UserPreferencesViewModel.cs" company="Jim Borden">
// Copyright (c) Jim Borden. All rights reserved.
// Licensed under the GPL-3.0 license. See LICENSE.md file in the project root for full license information.
// </copyright>
// -----------------------------------------------------------------------

using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using Spectre.Models;
using Spectre.Net.Api;
using Spectre.Services;

namespace Spectre.ViewModels;

/// <summary>
/// The view model for the user preferences page of the app.
/// </summary>
/// <param name="navigation">The object that performs page to page navigation.</param>
public sealed partial class UserPreferencesViewModel(INavigationService navigation) : ObservableObject
{
    private readonly INavigationService _navigation = navigation;
    private readonly UserData _userData = new();

    [ObservableProperty]
    private string _userName = string.Empty;

    [ObservableProperty]
    private PasswordType _selectedPasswordType = new(SpectreResultType.Long);

    [ObservableProperty]
    private SpectreAlgorithmVersion _selectedAlgorithm = SpectreAlgorithmVersion.V3;

    [ObservableProperty]
    private bool _hiddenPasswords;

    /// <summary>
    /// Gets the possible algorithm choices that the user can use as a default.
    /// </summary>
    public IList<SpectreAlgorithmVersion> AlgorithmChoices { get; } = new List<SpectreAlgorithmVersion> { SpectreAlgorithmVersion.V3 };

    /// <summary>
    /// Gets the possible password type choices that the user can use as a default.
    /// </summary>
    public IList<PasswordType> PasswordTypeChoices { get; } = new List<PasswordType>
    {
        new(SpectreResultType.Maximum),
        new(SpectreResultType.Long),
        new(SpectreResultType.Medium),
        new(SpectreResultType.Short),
        new(SpectreResultType.Basic),
        new(SpectreResultType.PIN),
        new(SpectreResultType.Name),
        new(SpectreResultType.Phrase),
        new(SpectreResultType.Personal),
        new(SpectreResultType.Device),
        new(SpectreResultType.DeriveKey)
    };

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