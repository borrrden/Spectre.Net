namespace Spectre.Net.Api
{
    public interface ISpectreUserKey
    {
        string ID { get; }

        SpectreAlgorithmVersion Version { get; }

        ISpectreSiteKey CreateSiteKey(string siteName, SpectreCounter keyCounter, SpectreKeyPurpose keyPurpose, string? keyContext);
    }
}
