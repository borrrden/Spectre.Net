// -----------------------------------------------------------------------
// <copyright file="SpectreCounter.cs" company="Jim Borden">
// Copyright (c) Jim Borden. All rights reserved.
// Licensed under the GPL-3.0 license. See LICENSE.md file in the project root for full license information.
// </copyright>
// -----------------------------------------------------------------------

namespace Spectre.Net.Api;

/// <summary>
/// Either zero, which is a one time password generator, or a non negative number indicating
/// a counter to use when deriving the final password.
/// </summary>
public enum SpectreCounter : uint
{
    /// <summary>
    /// Use a time-based counter value, resulting in a TOTP generator.
    /// </summary>
    TOTP = 0,

    /// <summary>
    /// The initial value for a site's counter.
    /// </summary>
    Initial = 1,

    /// <summary>
    /// The default value for this enumeration.
    /// </summary>
    Default = Initial,

    /// <summary>
    /// The lowest allowed value for this enumeration.
    /// </summary>
    First = TOTP,

    /// <summary>
    /// The highest allowed value for this enumeration.
    /// </summary>
    Last = uint.MaxValue
}