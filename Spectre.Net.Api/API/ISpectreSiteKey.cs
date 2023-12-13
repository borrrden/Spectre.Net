// -----------------------------------------------------------------------
// <copyright file="ISpectreSiteKey.cs" company="Jim Borden">
// Copyright (c) Jim Borden. All rights reserved.
// Licensed under the GPL-3.0 license. See LICENSE.md file in the project root for full license information.
// </copyright>
// -----------------------------------------------------------------------

namespace Spectre.Net.Api;

/// <summary>
/// An interface of an object that represents derived data for a site.  It is capable
/// of generating a password of a given type.
/// </summary>
public interface ISpectreSiteKey
{
    /// <summary>
    /// Gets the unique ID of the key.
    /// </summary>
    string ID { get; }

    /// <summary>
    /// Gets the version used to generate the key.
    /// </summary>
    SpectreAlgorithmVersion Version { get; }

    /// <summary>
    /// Creates a password of the given type.
    /// </summary>
    /// <param name="type">The type to generate.</param>
    /// <returns>The generated password.</returns>
    string CreatePassword(SpectreResultType type);
}