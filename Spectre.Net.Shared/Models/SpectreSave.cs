using System;
using System.Collections.Generic;

namespace Spectre.Models;

public sealed class SpectreSave
{
    public ExportData Export { get; init; } = new ExportData();

    public UserData User { get; init; } = new UserData();

    public IReadOnlyDictionary<string, SiteData> Sites { get; init; } = new Dictionary<string, SiteData>();
}

public sealed class ExportData
{
    public DateTimeOffset Date { get; init; } = new DateTimeOffset();

    public bool Redacted { get; init; } = true;

    public int Format { get; init; } = 0;
}

public sealed class UserData
{
    public int Avatar { get; init; } = 0;

    public string FullName { get; set; } = "";

    public string Identicon { get; init; } = "";

    public bool HidePasswords { get; set; } = false;

    public int Algorithm { get; set; } = 0;

    public string KeyID { get; set; } = "";

    public int DefaultType { get; set; } = 0;

    public int LoginType { get; init; } = 0;

    public DateTimeOffset LastUsed { get; set; } = new DateTimeOffset();
}

public sealed class SiteData
{
    public int Counter { get; set; } = 0;

    public int Algorithm { get; set; } = 0;

    public int Type { get; set; } = 0;

    public int LoginType { get; init; } = 0;

    public int Uses { get; set; } = 0;

    public DateTimeOffset LastUsed { get; set; } = new DateTimeOffset();
}