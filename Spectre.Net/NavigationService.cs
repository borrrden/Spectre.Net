// -----------------------------------------------------------------------
// <copyright file="NavigationService.cs" company="Jim Borden">
// Copyright (c) Jim Borden. All rights reserved.
// Licensed under the GPL-3.0 license. See LICENSE.md file in the project root for full license information.
// </copyright>
// -----------------------------------------------------------------------

namespace Spectre.Services;

internal sealed class NavigationService : INavigationService
{
    public async Task BackAsync(IDictionary<string, object>? data = null)
    {
        await GoToAsync("..", data);
    }

    public async Task GoToAsync(string name, IDictionary<string, object>? data = null)
    {
        if (data == null) {
            await Shell.Current.GoToAsync(name);
        } else {
            await Shell.Current.GoToAsync(name, data);
        }
    }
}