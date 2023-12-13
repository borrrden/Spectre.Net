// -----------------------------------------------------------------------
// <copyright file="LauncherService.cs" company="Jim Borden">
// Copyright (c) Jim Borden. All rights reserved.
// Licensed under the GPL-3.0 license. See LICENSE.md file in the project root for full license information.
// </copyright>
// -----------------------------------------------------------------------

namespace Spectre.Services;

internal sealed class LauncherService : ILauncherService
{
    public Task OpenAsync(string url) => Launcher.OpenAsync(url);
}