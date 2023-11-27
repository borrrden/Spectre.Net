using Microsoft.Extensions.Logging;

using Spectre.Net.Api.Algorithms;

namespace Spectre.Net.Api
{
    public static class SpectreAlgorithm
    {
        private static ILogger _logger = SpectreUtil.LoggerFactory.CreateLogger(nameof(SpectreAlgorithm));

        public static ISpectreUserKey CreateUserKey(string username, string userSecret, SpectreAlgorithmVersion version)
        {
            if (username == null) throw new ArgumentNullException(nameof(username));
            if (userSecret == null) throw new ArgumentNullException(nameof(userSecret));
            _logger.LogTrace("-- UserKey (algorithm: {0})", version);
            _logger.LogTrace("username: {0}", username);
            _logger.LogTrace("userSecret: {0}", SpectreUtil.StringSHA256(userSecret));
            switch (version)
            {
                case SpectreAlgorithmVersion.v3:
                    return new SpectreUserKey_v3(username, userSecret);
                default:
                    var e = new NotSupportedException($"Unsupported algorithm: {version}");
                    _logger.LogError(e, e.Message);
                    throw e;
            }
        }
    }
}
