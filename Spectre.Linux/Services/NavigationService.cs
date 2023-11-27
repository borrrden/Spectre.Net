using Avalonia;
using Avalonia.Controls.ApplicationLifetimes;

using CommunityToolkit.Mvvm.ComponentModel;

using Microsoft.Extensions.DependencyInjection;

using Spectre.Services;
using Spectre.ViewModels;

using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Spectre.Linux.Services
{
    internal sealed class NavigationService : INavigationService
    {
        private static IDictionary<string, Type> RouteMap = new Dictionary<string, Type>();

        private Stack<ObservableObject> _pages = new Stack<ObservableObject>();

        public MainWindowViewModel? RootViewModel { get; set; }

        public static void RegisterRoute<T>(string name) where T : ObservableObject
        {
            RouteMap[name] = typeof(T);
        }

        public Task BackAsync(IDictionary<string, object>? data = null)
        {
            if (RootViewModel == null)
            {
                throw new InvalidOperationException("RootViewModel not set!");
            }

            RootViewModel.ContentViewModel = _pages.Pop();
            return Task.CompletedTask;
        }

        public Task GoToAsync(string name, IDictionary<string, object>? data = null)
        {
            if (RootViewModel == null)
            {
                throw new InvalidOperationException("RootViewModel not set!");
            }

            if (!RouteMap.TryGetValue(name, out var vmType)) {
                throw new InvalidOperationException($"No route registered for {name}");
            }

            _pages.Push(RootViewModel.ContentViewModel);
            RootViewModel.ContentViewModel = (ObservableObject)App.Services.GetRequiredService(vmType);
            return Task.CompletedTask;
        }
    }
}
