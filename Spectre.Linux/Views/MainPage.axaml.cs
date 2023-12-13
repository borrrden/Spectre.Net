// -----------------------------------------------------------------------
// <copyright file="MainPage.axaml.cs" company="Jim Borden">
// Copyright (c) Jim Borden. All rights reserved.
// Licensed under the GPL-3.0 license. See LICENSE.md file in the project root for full license information.
// </copyright>
// -----------------------------------------------------------------------

using Avalonia.Controls;
using Avalonia.Interactivity;

namespace Spectre.Linux.Views;

/// <summary>
/// The main page of the application.
/// </summary>
public partial class MainPage : UserControl
{
    /// <summary>
    /// Initializes a new instance of the <see cref="MainPage"/> class.
    /// </summary>
    public MainPage()
    {
        InitializeComponent();
    }

    /// <inheritdoc />
    protected override void OnLoaded(RoutedEventArgs e)
    {
        base.OnLoaded(e);

        _userPicker.SelectedIndex = 0;
    }
}