// -----------------------------------------------------------------------
// <copyright file="PasswordType.cs" company="Jim Borden">
// Copyright (c) Jim Borden. All rights reserved.
// Licensed under the GPL-3.0 license. See LICENSE.md file in the project root for full license information.
// </copyright>
// -----------------------------------------------------------------------

using Spectre.Net.Api;

namespace Spectre.ViewModels;

/// <summary>
/// A class that allows a <see cref="SpectreResultType"/> to be represented as a string.
/// </summary>
/// <param name="type">The type to represent.</param>
public sealed class PasswordType(SpectreResultType type)
{
    private static readonly IReadOnlyDictionary<SpectreResultType, string> DisplayNameMapping = new Dictionary<SpectreResultType, string>
    {
        [SpectreResultType.Maximum] = "Maximum Security",
        [SpectreResultType.Long] = "Long Password",
        [SpectreResultType.Medium] = "Medium Password",
        [SpectreResultType.Short] = "Short Password",
        [SpectreResultType.Basic] = "Basic Password",
        [SpectreResultType.PIN] = "PIN Code",
        [SpectreResultType.Name] = "Name",
        [SpectreResultType.Phrase] = "Phrase",
        [SpectreResultType.Personal] = "Saved",
        [SpectreResultType.Device] = "Private",
        [SpectreResultType.DeriveKey] = "Binary Key"
    };

    /// <summary>
    /// Gets the type that was given when the object was created.
    /// </summary>
    public SpectreResultType ResultType { get; } = type;

    /// <summary>
    /// Gets the name to display in the UI.
    /// </summary>
    public string DisplayName => DisplayNameMapping[ResultType];

    /// <inheritdoc />
    public override int GetHashCode()
    {
        return ResultType.GetHashCode();
    }

    /// <inheritdoc />
    public override bool Equals(object? obj)
    {
        if (obj is not PasswordType other) {
            return false;
        }

        return other.ResultType == ResultType;
    }
}