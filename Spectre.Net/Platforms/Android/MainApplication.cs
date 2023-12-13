// -----------------------------------------------------------------------
// <copyright file="MainApplication.cs" company="Jim Borden">
// Copyright (c) Jim Borden. All rights reserved.
// Licensed under the GPL-3.0 license. See LICENSE.md file in the project root for full license information.
// </copyright>
// -----------------------------------------------------------------------

using Android.App;
using Android.Runtime;

namespace Spectre.Net;

/// <summary>
/// The overall Android application logic.
/// </summary>
/// <param name="handle">Used by runtime.</param>
/// <param name="ownership">Used by runtime to transfer ownership.</param>
[Application]
public class MainApplication(IntPtr handle, JniHandleOwnership ownership) : MauiApplication(handle, ownership)
{
    /// <inheritdoc />
    protected override MauiApp CreateMauiApp() => MauiProgram.CreateMauiApp();
}