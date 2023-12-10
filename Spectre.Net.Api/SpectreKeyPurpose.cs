// -----------------------------------------------------------------------
// <copyright file="SpectreKeyPurpose.cs" company="Jim Borden">
// Copyright (c) Jim Borden. All rights reserved.
// Licensed under the GPL-3.0 license. See LICENSE.md file in the project root for full license information.
// </copyright>
// -----------------------------------------------------------------------

namespace Spectre.Net.Api;

public enum SpectreKeyPurpose : byte
{
    /** Generate a key for authentication. */
    Authentication,
    /** Generate a name for identification. */
    Identification,
    /** Generate a recovery token. */
    Recovery
}
