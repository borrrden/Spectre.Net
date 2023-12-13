// -----------------------------------------------------------------------
// <copyright file="SpectreUtil.cs" company="Jim Borden">
// Copyright (c) Jim Borden. All rights reserved.
// Licensed under the GPL-3.0 license. See LICENSE.md file in the project root for full license information.
// </copyright>
// -----------------------------------------------------------------------

using System.Security.Cryptography;
using System.Text;
using Microsoft.Extensions.Logging;

namespace Spectre.Net.Api;

/// <summary>
/// A collection of utility items used by the library.
/// </summary>
public static class SpectreUtil
{
    private static ILoggerFactory? _loggerFactory;

    /// <summary>
    /// Gets or sets the <see cref="ILoggerFactory"/> that the library will use to
    /// generate loggers.
    /// </summary>
    public static ILoggerFactory LoggerFactory
    {
        get
        {
            _loggerFactory ??= Microsoft.Extensions.Logging.LoggerFactory.Create(static builder =>
                {
                    builder.AddConsole().AddDebug().SetMinimumLevel(LogLevel.Warning);
                });

            return _loggerFactory;
        }
        set => _loggerFactory = value;
    }

    internal static byte[] HashSHA256(string input)
    {
        return SHA256.HashData(Encoding.UTF8.GetBytes(input));
    }

    internal static string StringSHA256(string input)
    {
        return Convert.ToHexString(HashSHA256(input));
    }

    internal static string StringSHA256(byte[] input)
    {
        return Convert.ToHexString(SHA256.HashData(input));
    }
}