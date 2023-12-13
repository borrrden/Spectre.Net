// -----------------------------------------------------------------------
// <copyright file="SpectreKeyPurpose.cs" company="Jim Borden">
// Copyright (c) Jim Borden. All rights reserved.
// Licensed under the GPL-3.0 license. See LICENSE.md file in the project root for full license information.
// </copyright>
// -----------------------------------------------------------------------

namespace Spectre.Net.Api;

/// <summary>
/// A purpose for deriving a secret.
/// </summary>
public enum SpectreKeyPurpose : byte
{
    /// <summary>
    /// Generate a key for authentication.
    /// </summary>
    Authentication,

    /// <summary>
    /// Generate a name for identification.
    /// </summary>
    Identification,

    /// <summary>
    /// Generate a recovery token.
    /// </summary>
    Recovery
}