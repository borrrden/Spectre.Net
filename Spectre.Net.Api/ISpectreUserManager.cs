// -----------------------------------------------------------------------
// <copyright file="ISpectreUserManager.cs" company="Jim Borden">
// Copyright (c) Jim Borden. All rights reserved.
// Licensed under the GPL-3.0 license. See LICENSE.md file in the project root for full license information.
// </copyright>
// -----------------------------------------------------------------------

using System.Text.Json;

namespace Spectre.Net.Api;

/// <summary>
/// An interface for managing persistent users on disk.
/// </summary>
public interface ISpectreUserManager
{
    /// <summary>
    /// Reads a given user's information from disk.
    /// </summary>
    /// <param name="username">The user to read information for.</param>
    /// <returns>The user's information.</returns>
    SpectreSave? ReadUser(string username);

    /// <summary>
    /// Reads a given user's information from disk as an unparsed string.
    /// </summary>
    /// <param name="username">The user to read information for.</param>
    /// <returns>The user's information.</returns>
    string? ReadUserRaw(string username);

    /// <summary>
    /// Creates a new user on disk.
    /// </summary>
    /// <param name="username">The username of the user to create.</param>
    /// <param name="key">The key that the user has generated.</param>
    /// <returns>The user information as it would be read from disk.</returns>
    SpectreSave CreateUser(string username, ISpectreUserKey key);

    /// <summary>
    /// Updates a user's information on disk.
    /// </summary>
    /// <param name="username">The username of the user to modify.</param>
    /// <param name="save">The new data to store for the user.</param>
    void UpdateUser(string username, SpectreSave save);

    /// <summary>
    /// Deletes a user's information from disk.
    /// </summary>
    /// <param name="username">The username of the user to delete.</param>
    void DeleteUser(string username);

    /// <summary>
    /// Gets all of the users known to the library.
    /// </summary>
    /// <returns>An enumerable of the usernames saved to disk.</returns>
    IEnumerable<string> AllUserNames();
}

/// <summary>
/// The default implementation of <see cref="ISpectreUserManager"/>.  It should be registered in any
/// dependency injection containers for normal use.
/// </summary>
public sealed class SpectreUserManager : ISpectreUserManager
{
    private static readonly string SpectreDataPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.UserProfile), ".spectre.d");
    private readonly JsonSerializerOptions _jsonOptions = new() {
        PropertyNamingPolicy = JsonNamingPolicy.SnakeCaseLower,
        WriteIndented = true
    };

    /// <summary>
    /// Initializes a new instance of the <see cref="SpectreUserManager"/> class.
    /// </summary>
    public SpectreUserManager()
    {
        if (!Directory.Exists(SpectreDataPath)) {
            Directory.CreateDirectory(SpectreDataPath);
        }

        _jsonOptions.Converters.Add(new ISO8601DateTimeConverter());
    }

    /// <inheritdoc />
    public IEnumerable<string> AllUserNames()
    {
        return Directory.EnumerateFiles(SpectreDataPath).Select(x => Path.GetFileNameWithoutExtension(x));
    }

    /// <inheritdoc />
    public SpectreSave CreateUser(string username, ISpectreUserKey key)
    {
        var exportData = new ExportData
        {
            Date = DateTimeOffset.UtcNow,
            Redacted = true,
            Format = 2
        };

        var userData = new UserData
        {
            FullName = username,
            Algorithm = 3,
            KeyID = key.ID,
            DefaultType = 17,
            LoginType = 30,
            LastUsed = exportData.Date
        };

        var spectreSave = new SpectreSave
        {
            User = userData,
            Export = exportData
        };

        var saveFilePath = Path.Combine(SpectreDataPath, $"{username}.mpjson");
        using var fout = File.OpenWrite(saveFilePath);
        JsonSerializer.Serialize(fout, spectreSave, _jsonOptions);
        return spectreSave;
    }

    /// <inheritdoc />
    public void UpdateUser(string username, SpectreSave save)
    {
        var saveFilePath = Path.Combine(SpectreDataPath, $"{username}.mpjson");
        File.Delete(saveFilePath);
        using var fout = File.OpenWrite(saveFilePath);
        JsonSerializer.Serialize(fout, save, _jsonOptions);
    }

    /// <inheritdoc />
    public void DeleteUser(string username)
    {
        var saveFilePath = Path.Combine(SpectreDataPath, $"{username}.mpjson");
        File.Delete(saveFilePath);
    }

    /// <inheritdoc />
    public SpectreSave? ReadUser(string username)
    {
        var saveFilePath = Path.Combine(SpectreDataPath, $"{username}.mpjson");
        if (!File.Exists(saveFilePath)) {
            return null;
        }

        using var fin = File.OpenRead(saveFilePath);
        return JsonSerializer.Deserialize<SpectreSave>(fin, _jsonOptions)!;
    }

    /// <inheritdoc />
    public string? ReadUserRaw(string username)
    {
        var saveFilePath = Path.Combine(SpectreDataPath, $"{username}.mpjson");
        if (!File.Exists(saveFilePath)) {
            return null;
        }

        return File.ReadAllText(saveFilePath);
    }
}