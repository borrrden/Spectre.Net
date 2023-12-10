// -----------------------------------------------------------------------
// <copyright file="SiteSettingsViewModel.cs" company="Jim Borden">
// Copyright (c) Jim Borden. All rights reserved.
// Licensed under the GPL-3.0 license. See LICENSE.md file in the project root for full license information.
// </copyright>
// -----------------------------------------------------------------------

using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using Spectre.Net.Api;
using Spectre.Services;

namespace Spectre.ViewModels;

/// <summary>
/// The view model for the site settings page of the app.
/// </summary>
public sealed partial class SiteSettingsViewModel : ObservableObject
{
    private readonly INavigationService _navigation;

    [ObservableProperty]
    private string _siteName = "test";

    [ObservableProperty]
    private PasswordType _selectedPasswordType = new(SpectreResultType.Long);

    [ObservableProperty]
    private SpectreAlgorithmVersion _selectedAlgorithm;

    [ObservableProperty]
    private int _counterValue;
    private SiteData _siteData = new();

    /// <summary>
    /// Initializes a new instance of the <see cref="SiteSettingsViewModel"/> class.
    /// </summary>
    /// <param name="navigation">The object that performs page to page navigation.</param>
    public SiteSettingsViewModel(INavigationService navigation)
    {
        _navigation = navigation;
        WeakReferenceMessenger.Default.Register<SiteSettingsViewModel, ApplySiteSettingsMessage>(this, (r, e) => r.ApplySiteSettings(e.Value));
    }

    /// <summary>
    /// Gets the possible algorithm choices for the site.
    /// </summary>
    public IList<SpectreAlgorithmVersion> AlgorithmChoices { get; } = new List<SpectreAlgorithmVersion> { SpectreAlgorithmVersion.V3 };

    /// <summary>
    /// Gets the possible password type choices for the site.
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

    internal void ApplySiteSettings(SiteNameAndData siteNameAndData)
    {
        SiteName = siteNameAndData.SiteName;
        var siteData = siteNameAndData.SiteData;
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