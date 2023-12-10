// -----------------------------------------------------------------------
// <copyright file="NewUserMessage.cs" company="Jim Borden">
// Copyright (c) Jim Borden. All rights reserved.
// Licensed under the GPL-3.0 license. See LICENSE.md file in the project root for full license information.
// </copyright>
// -----------------------------------------------------------------------

using CommunityToolkit.Mvvm.Messaging.Messages;
using Spectre.Models;

namespace Spectre.ViewModels;

/// <summary>
/// A message indicating that a new user has been created by the user of the app.
/// </summary>
/// <param name="value">The created user object.</param>
public sealed class NewUserMessage(UserListEntry value) : ValueChangedMessage<UserListEntry>(value)
{
}