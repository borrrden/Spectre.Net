using Spectre.Net.Api;

namespace Spectre.Services;

public interface ISpectreUserKeyProvider
{
    public ISpectreUserKey CreateUserKey(string username, string userSecret, SpectreAlgorithmVersion version);
}

public sealed class SpectreUserKeyProvider : ISpectreUserKeyProvider
{
    public ISpectreUserKey CreateUserKey(string username, string userSecret, SpectreAlgorithmVersion version)
    {
        return SpectreAlgorithm.CreateUserKey(username, userSecret, version);
    }
}
