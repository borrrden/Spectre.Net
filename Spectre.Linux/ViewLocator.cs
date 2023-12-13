// -----------------------------------------------------------------------
// <copyright file="ViewLocator.cs" company="Jim Borden">
// Copyright (c) Jim Borden. All rights reserved.
// Licensed under the GPL-3.0 license. See LICENSE.md file in the project root for full license information.
// </copyright>
// -----------------------------------------------------------------------

using Avalonia.Controls;
using Avalonia.Controls.Templates;

using CommunityToolkit.Mvvm.ComponentModel;

namespace Spectre.Linux;

/// <summary>
/// The class responsible for matching view models with their respective pages.
/// </summary>
public class ViewLocator : IDataTemplate
{
    /// <inheritdoc />
    public Control? Build(object? data)
    {
        if (data is null) {
            return null;
        }

        var name = data.GetType().FullName!.Replace("ViewModels", "Linux.Views").Replace("Page", string.Empty).Replace("ViewModel", "Page", StringComparison.Ordinal);
        var type = Type.GetType(name);

        if (type != null) {
            var control = (Control)Activator.CreateInstance(type)!;
            control.DataContext = data;
            return control;
        }

        return new TextBlock { Text = "Not Found: " + name };
    }

    /// <inheritdoc />
    public bool Match(object? data)
    {
        return data is ObservableObject;
    }
}