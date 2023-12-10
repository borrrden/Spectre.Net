// -----------------------------------------------------------------------
// <copyright file="UserDefaults.cs" company="Jim Borden">
// Copyright (c) Jim Borden. All rights reserved.
// Licensed under the GPL-3.0 license. See LICENSE.md file in the project root for full license information.
// </copyright>
// -----------------------------------------------------------------------

using CommunityToolkit.Mvvm.ComponentModel;
using Spectre.Net.Api;

namespace Spectre.Models;

/// <summary>
/// A class which stores and reacts to changes in the default settings for a given user.
/// </summary>
public sealed partial class UserDefaults : ObservableObject
{
    /// <summary>
    /// Gets the singleton instantions of <see cref="UserDefaults"/>.
    /// </summary>
    public static readonly UserDefaults Current = new();

    [ObservableProperty]
    private SpectreAlgorithmVersion _algorithmVersion = SpectreAlgorithmVersion.Current;

    [ObservableProperty]
    private SpectreResultType _defaultPasswordType = SpectreResultType.Long;

    [ObservableProperty]
    private bool _hiddenPasswords;

    private UserDefaults()
    {
    }

    /// <summary>
    /// Applies the given user data.
    /// </summary>
    /// <param name="userData">The user data as read from disk.</param>
    public void ReadDefaults(UserData userData)
    {
        // Don't want to fire the event here
#pragma warning disable MVVMTK0034 // Direct field reference to [ObservableProperty] backing field
        _algorithmVersion = (SpectreAlgorithmVersion)userData.Algorithm;
        _hiddenPasswords = userData.HidePasswords;
        _defaultPasswordType = (SpectreResultType)userData.DefaultType;
#pragma warning restore MVVMTK0034 // Direct field reference to [ObservableProperty] backing field
    }
}