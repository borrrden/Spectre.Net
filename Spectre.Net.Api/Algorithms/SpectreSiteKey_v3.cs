using Microsoft.Extensions.Logging;

using System.Security.Cryptography;
using System.Text;

namespace Spectre.Net.Api.Algorithms
{
    internal sealed class SpectreSiteKey_v3 : ISpectreSiteKey
    {
        private byte[] _keyMaterial;
        private readonly ILogger<SpectreSiteKey_v3> _logger = SpectreUtil.LoggerFactory.CreateLogger<SpectreSiteKey_v3>();

        public string ID { get; }

        public SpectreAlgorithmVersion Version => SpectreAlgorithmVersion.v3;

        public SpectreSiteKey_v3(byte[] keyMaterial, string siteName, SpectreCounter keyCounter, SpectreKeyPurpose keyPurpose, string? keyContext)
        {
            var scope = keyPurpose.ToPurposeScope();
            _logger.LogTrace("keyScope: {0}", scope);
            if (keyCounter == SpectreCounter.TOTP) throw new NotImplementedException();

            _logger.LogTrace("siteSalt: keyScope={0} | #siteName={1:X9} | siteName={2} | keyCounter={3:X9} | #keyContext={4:X9} | keyContext={5}",
                scope, siteName.Length, siteName, ((int)keyCounter).ToString("X9"), keyContext?.Length ?? 0, keyContext);

            using var siteSaltStream = new MemoryStream();
            siteSaltStream.Write(Encoding.ASCII.GetBytes(scope));
            siteSaltStream.Write(BitConverter.GetBytes(siteName.Length).Reverse().ToArray());
            siteSaltStream.Write(Encoding.ASCII.GetBytes(siteName));
            siteSaltStream.Write(BitConverter.GetBytes((uint)keyCounter).Reverse().ToArray());
            if (keyContext != null)
            {
                siteSaltStream.Write(BitConverter.GetBytes(keyContext.Length).Reverse().ToArray());
                siteSaltStream.Write(Encoding.ASCII.GetBytes(keyContext));
            }

            var siteSalt = siteSaltStream.ToArray();
            _logger.LogTrace("siteSalt.id: {0}", Convert.ToHexString(SpectreUtil.HashSHA256(siteSalt)));

            using var hmac = new HMACSHA256(keyMaterial);
            _keyMaterial = hmac.ComputeHash(siteSalt);
            ID = SpectreUtil.StringSHA256(_keyMaterial);
            _logger.LogTrace("siteKey.id: {0}", this);
        }

        public string CreatePassword(SpectreResultType type)
        {
            var seedByte = _keyMaterial[0];
            var template = SpectreType.GetTemplate(type, seedByte);
            var sitePassword = new StringBuilder(template.Length);
            for (var i = 0; i < template.Length; i++)
            {
                seedByte = _keyMaterial[i + 1];
                sitePassword.Append(SpectreType.GetClassCharacter(template[i], seedByte));
            }

            return sitePassword.ToString();
        }

        public override int GetHashCode()
        {
            return ID.GetHashCode();
        }

        public override string ToString()
        {
            return ID;
        }
    }
}
