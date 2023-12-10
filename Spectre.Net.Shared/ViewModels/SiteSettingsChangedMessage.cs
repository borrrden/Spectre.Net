// -----------------------------------------------------------------------
// <copyright file="SiteSettingsChangedMessage.cs" company="Jim Borden">
// Copyright (c) Jim Borden. All rights reserved.
// Licensed under the GPL-3.0 license. See LICENSE.md file in the project root for full license information.
// </copyright>
// -----------------------------------------------------------------------

using CommunityToolkit.Mvvm.Messaging.Messages;

namespace Spectre.ViewModels;

/// <summary>
/// A message indicating that the settings for a particular site have changed.
/// </summary>
/// <param name="value">The name of the site that changed.</param>
public sealed class SiteSettingsChangedMessage(string value) : ValueChangedMessage<string>(value)
{
}