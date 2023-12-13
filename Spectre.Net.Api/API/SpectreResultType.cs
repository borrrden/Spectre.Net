// -----------------------------------------------------------------------
// <copyright file="SpectreResultType.cs" company="Jim Borden">
// Copyright (c) Jim Borden. All rights reserved.
// Licensed under the GPL-3.0 license. See LICENSE.md file in the project root for full license information.
// </copyright>
// -----------------------------------------------------------------------

namespace Spectre.Net.Api;

/// <summary>
/// An enumeration of the possible ways to derive a final password or key data, and some
/// properties of how they are saved or not saved.
/// </summary>
public enum SpectreResultType : uint
{
    /// <summary>
    /// 0: Don't produce a result.
    /// </summary>
    None = 0,

    /// <summary>
    /// Use the site key to generate a result from a template.
    /// </summary>
    TemplateClass = 1 << 4,

    /// <summary>
    /// Use the site key to encrypt and decrypt a stateful entity.
    /// </summary>
    StatefulClass = 1 << 5,

    /// <summary>
    /// Use the site key to derive a site-specific object.
    /// </summary>
    DeriveClass = 1 << 6,

    /// <summary>
    /// Export the key-protected content data.
    /// </summary>
    ExportContentFeature = 1 << 10,

    /// <summary>
    /// Never export content.
    /// </summary>
    DevicePrivateFeature = 1 << 11,

    /// <summary>
    /// Don't use this as the primary authentication result type.
    /// </summary>
    AlternateFeature = 1 << 12,

    /// <summary>
    /// 16: pg^VMAUBk5x3p%HP%i4= .
    /// </summary>
#pragma warning disable CA1069 // Enums values should not be duplicated
    Maximum = 0x0 | TemplateClass | None,
#pragma warning restore CA1069 // Enums values should not be duplicated

    /// <summary>
    /// 17: BiroYena8:Kixa .
    /// </summary>
    Long = 0x1 | TemplateClass | None,

    /// <summary>
    /// 18: BirSuj0- .
    /// </summary>
    Medium = 0x2 | TemplateClass | None,

    /// <summary>
    /// 19: Bir8 .
    /// </summary>
    Short = 0x3 | TemplateClass | None,

    /// <summary>
    /// 20: pO98MoD0 .
    /// </summary>
    Basic = 0x4 | TemplateClass | None,

    /// <summary>
    /// 21: 2798 .
    /// </summary>
    PIN = 0x5 | TemplateClass | None,

    /// <summary>
    /// 30: birsujano .
    /// </summary>
    Name = 0xE | TemplateClass | None,

    /// <summary>
    /// 31: bir yennoquce fefi .
    /// </summary>
    Phrase = 0xF | TemplateClass | None,

    /// <summary>
    /// 1056: Custom saved result.
    /// </summary>
    Personal = 0x0 | StatefulClass | ExportContentFeature,

    /// <summary>
    /// 2081: Custom saved result that should not be exported from the device.
    /// </summary>
    Device = 0x1 | StatefulClass | DevicePrivateFeature,

    /// <summary>
    /// 4160: Derive a unique binary key. .
    /// </summary>
    DeriveKey = 0x0 | DeriveClass | AlternateFeature,

    /// <summary>
    /// The default mode to use when deriving a password.
    /// </summary>
    DefaultResult = Long,

    /// <summary>
    /// The default mode to use when deriving a login name.
    /// </summary>
    DefaultLogin = Name
}