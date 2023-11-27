namespace Spectre.Net.Api
{
    public interface ISpectreSiteKey
    {
        string ID { get; }

        SpectreAlgorithmVersion Version { get; }

        string CreatePassword(SpectreResultType type);
    }
}
