// -----------------------------------------------------------------------
// <copyright file="EnumExtensions.cs" company="Jim Borden">
// Copyright (c) Jim Borden. All rights reserved.
// Licensed under the GPL-3.0 license. See LICENSE.md file in the project root for full license information.
// </copyright>
// -----------------------------------------------------------------------

namespace Spectre.Net.Api;

/// <summary>
/// A class of extensions for the enumerations in this library.
/// </summary>
public static class EnumExtensions
{
    /// <summary>
    /// Converts a <see cref="SpectreKeyPurpose"/> to a long form string for use in derivation.
    /// </summary>
    /// <param name="purpose">The key purpose to convert.</param>
    /// <returns>A long form string for use in key derivation.</returns>
    /// <exception cref="ArgumentException">An invalid purpose was passed.</exception>
    public static string ToPurposeScope(this SpectreKeyPurpose purpose)
    {
        return purpose switch
        {
            SpectreKeyPurpose.Authentication => "com.lyndir.masterpassword",
            SpectreKeyPurpose.Identification => "com.lyndir.masterpassword.login",
            SpectreKeyPurpose.Recovery => "com.lyndir.masterpassword.answer",
            _ => throw new ArgumentException($"Unknown purpose {purpose}"),
        };
    }
}