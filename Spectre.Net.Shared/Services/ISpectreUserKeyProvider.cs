// -----------------------------------------------------------------------
// <copyright file="ISpectreUserKeyProvider.cs" company="Jim Borden">
// Copyright (c) Jim Borden. All rights reserved.
// Licensed under the GPL-3.0 license. See LICENSE.md file in the project root for full license information.
// </copyright>
// -----------------------------------------------------------------------

using Spectre.Net.Api;

namespace Spectre.Services;

/// <summary>
/// An interface for an object that can create derived key material for a user.
/// </summary>
public interface ISpectreUserKeyProvider
{
    /// <summary>
    /// Derive a user key using the provided information.
    /// </summary>
    /// <param name="username">The username of the user.</param>
    /// <param name="userSecret">The master password of the user.</param>
    /// <param name="version">The version of the algorithm to use.</param>
    /// <returns>The created user key.</returns>
    public ISpectreUserKey CreateUserKey(string username, string userSecret, SpectreAlgorithmVersion version);
}

/// <summary>
/// The default implementation of <see cref="ISpectreUserKeyProvider"/>.
/// </summary>
public sealed class SpectreUserKeyProvider : ISpectreUserKeyProvider
{
    /// <inheritdoc />
    public ISpectreUserKey CreateUserKey(string username, string userSecret, SpectreAlgorithmVersion version)
    {
        return SpectreAlgorithm.CreateUserKey(username, userSecret, version);
    }
}