// -----------------------------------------------------------------------
// <copyright file="ApplySiteSettingsMessage.cs" company="Jim Borden">
// Copyright (c) Jim Borden. All rights reserved.
// Licensed under the GPL-3.0 license. See LICENSE.md file in the project root for full license information.
// </copyright>
// -----------------------------------------------------------------------

using CommunityToolkit.Mvvm.Messaging.Messages;
using Spectre.ViewModels;

namespace Spectre.ViewModels;

/// <summary>
/// A message indicating that site settigns need to be applied and the GUI updated.
/// </summary>
/// <param name="value">The name of the site and the new data it contains.</param>
public sealed class ApplySiteSettingsMessage(SiteNameAndData value) : ValueChangedMessage<SiteNameAndData>(value)
{
}