// -----------------------------------------------------------------------
// <copyright file="NavigationService.cs" company="Jim Borden">
// Copyright (c) Jim Borden. All rights reserved.
// Licensed under the GPL-3.0 license. See LICENSE.md file in the project root for full license information.
// </copyright>
// -----------------------------------------------------------------------

using CommunityToolkit.Mvvm.ComponentModel;

using Microsoft.Extensions.DependencyInjection;

using Spectre.Services;
using Spectre.ViewModels;

namespace Spectre.Linux.Services;

internal sealed class NavigationService : INavigationService
{
    private static readonly Dictionary<string, Type> _routeMap = [];

    private readonly Stack<ObservableObject> _pages = new();

    public MainWindowViewModel? RootViewModel { get; set; }

    public static void RegisterRoute<T>(string name)
        where T : ObservableObject
    {
        _routeMap[name] = typeof(T);
    }

    public Task BackAsync(IDictionary<string, object>? data = null)
    {
        if (RootViewModel == null) {
            throw new InvalidOperationException("RootViewModel not set!");
        }

        RootViewModel.ContentViewModel = _pages.Pop();
        return Task.CompletedTask;
    }

    public Task GoToAsync(string name, IDictionary<string, object>? data = null)
    {
        if (RootViewModel == null) {
            throw new InvalidOperationException("RootViewModel not set!");
        }

        if (!_routeMap.TryGetValue(name, out var vmType)) {
            throw new InvalidOperationException($"No route registered for {name}");
        }

        _pages.Push(RootViewModel.ContentViewModel);
        RootViewModel.ContentViewModel = (ObservableObject)App.Services.GetRequiredService(vmType);
        return Task.CompletedTask;
    }
}