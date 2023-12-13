// -----------------------------------------------------------------------
// <copyright file="AlertService.cs" company="Jim Borden">
// Copyright (c) Jim Borden. All rights reserved.
// Licensed under the GPL-3.0 license. See LICENSE.md file in the project root for full license information.
// </copyright>
// -----------------------------------------------------------------------

using Spectre.Services;

namespace Spectre.Linux.Services;

internal sealed class AlertService : IAlertService
{
    public Task<bool> DisplayAlert(string title, string message, string confirm, string reject)
    {
        throw new NotImplementedException();
    }
}