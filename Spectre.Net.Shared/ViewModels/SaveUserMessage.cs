// -----------------------------------------------------------------------
// <copyright file="SaveUserMessage.cs" company="Jim Borden">
// Copyright (c) Jim Borden. All rights reserved.
// Licensed under the GPL-3.0 license. See LICENSE.md file in the project root for full license information.
// </copyright>
// -----------------------------------------------------------------------

using CommunityToolkit.Mvvm.Messaging.Messages;

namespace Spectre.ViewModels;

/// <summary>
/// A message indicating that a user's data needs to be saved to disk.
/// </summary>
/// <param name="value"><c>true</c> if the user's data should be saved.</param>
public sealed class SaveUserMessage(bool value) : ValueChangedMessage<bool>(value)
{
}