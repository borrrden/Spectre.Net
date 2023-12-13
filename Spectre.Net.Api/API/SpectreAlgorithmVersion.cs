// -----------------------------------------------------------------------
// <copyright file="SpectreAlgorithmVersion.cs" company="Jim Borden">
// Copyright (c) Jim Borden. All rights reserved.
// Licensed under the GPL-3.0 license. See LICENSE.md file in the project root for full license information.
// </copyright>
// -----------------------------------------------------------------------

namespace Spectre.Net.Api;

/// <summary>
/// An enumeration of the Spectre user key derivation algortihm versions.
/// </summary>
public enum SpectreAlgorithmVersion : uint
{
    /// <summary>
    /// (2012-03-05) V0 incorrectly performed host-endian math with bytes translated into 16-bit network-endian.
    /// </summary>
    V0,

    /// <summary>
    /// (2012-07-17) V1 incorrectly sized site name fields by character count rather than byte count.
    /// </summary>
    V1,

    /// <summary>
    /// (2014-09-24) V2 incorrectly sized user name fields by character count rather than byte count.
    /// </summary>
    V2,

    /// <summary>
    /// (2015-01-15) V3 is the current version.
    /// </summary>
    V3,

    /// <summary>
    /// The current version of the algorithm (V3).
    /// </summary>
    Current = V3,

    /// <summary>
    /// The first version of the algorithm.
    /// </summary>
    First = V0,

    /// <summary>
    /// The latest version of the algorithm.
    /// </summary>
    Last = V3
}