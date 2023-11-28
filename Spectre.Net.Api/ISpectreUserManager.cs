using System.Text.Json;
using System.Text.Json.Serialization;

namespace Spectre.Net.Api;

internal sealed class ISO8601DateTimeConverter : JsonConverter<DateTimeOffset>
{
    public override DateTimeOffset Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        return DateTimeOffset.ParseExact(reader.GetString()!, "yyyy'-'MM'-'ddTHH':'mm':'ss'Z'", null);
    }

    public override void Write(Utf8JsonWriter writer, DateTimeOffset value, JsonSerializerOptions options)
    {
        writer.WriteStringValue(value.ToString("yyyy'-'MM'-'ddTHH':'mm':'ss'Z'"));
    }
}

public interface ISpectreUserManager
{
    SpectreSave? ReadUser(string username);

    string? ReadUserRaw(string username);

    SpectreSave CreateUser(string username, ISpectreUserKey key);

    void UpdateUser(string username, SpectreSave save);

    void DeleteUser(string username);

    IEnumerable<string> AllUserNames();
}

public sealed class SpectreUserManager : ISpectreUserManager
{

    private readonly string SpectreDataPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.UserProfile), ".spectre.d");
    private readonly JsonSerializerOptions _jsonOptions = new JsonSerializerOptions()
    {
        PropertyNamingPolicy = JsonNamingPolicy.SnakeCaseLower,
        WriteIndented = true
    };

    public SpectreUserManager() 
    {
        if (!Directory.Exists(SpectreDataPath))
        {
            Directory.CreateDirectory(SpectreDataPath);
        }

        _jsonOptions.Converters.Add(new ISO8601DateTimeConverter());
    }

    public IEnumerable<string> AllUserNames()
    {
        return Directory.EnumerateFiles(SpectreDataPath).Select(x => Path.GetFileNameWithoutExtension(x));
    }

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

    public void UpdateUser(string username, SpectreSave save)
    {
        var saveFilePath = Path.Combine(SpectreDataPath, $"{username}.mpjson");
        File.Delete(saveFilePath); using var fout = File.OpenWrite(saveFilePath);
        JsonSerializer.Serialize(fout, save, _jsonOptions);;
    }

    public void DeleteUser(string username)
    {
        var saveFilePath = Path.Combine(SpectreDataPath, $"{username}.mpjson");
        File.Delete(saveFilePath);
    }

    public SpectreSave? ReadUser(string username)
    {
        var saveFilePath = Path.Combine(SpectreDataPath, $"{username}.mpjson");
        if (!File.Exists(saveFilePath))
        {
            return null;
        }

        using var fin = File.OpenRead(saveFilePath);
        return JsonSerializer.Deserialize<SpectreSave>(fin, _jsonOptions)!;
    }

    public string? ReadUserRaw(string username)
    {
        var saveFilePath = Path.Combine(SpectreDataPath, $"{username}.mpjson");
        if (!File.Exists(saveFilePath))
        {
            return null;
        }

        return File.ReadAllText(saveFilePath);
    }
}
