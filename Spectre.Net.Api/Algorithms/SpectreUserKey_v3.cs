using Microsoft.Extensions.Logging;

using System.Runtime.InteropServices;
using System.Text;

namespace Spectre.Net.Api.Algorithms
{
    internal static class Interop
    {
#if IOS
        const string DllName = "__Internal";
#else
        const string DllName = "libsodium";
#endif

        [DllImport(DllName, CallingConvention = CallingConvention.Cdecl)]
        internal static extern int crypto_pwhash_scryptsalsa208sha256_ll(byte[] secret, nuint secretSize, byte[] salt, UIntPtr saltSize, ulong N, uint r, uint p, 
            [Out]byte[] key, nuint keySize);
    }

    internal sealed class SpectreUserKey_v3 : ISpectreUserKey
    {
        private readonly ILogger<SpectreUserKey_v3> _logger = SpectreUtil.LoggerFactory.CreateLogger<SpectreUserKey_v3>();
        private const int ScryptN = 32768;
        private const int ScryptR = 8;
        private const int ScryptP = 2;
        private const int ScryptDKLen = 64;

        private byte[] _keyMaterial;

        public string ID { get; }

        public SpectreAlgorithmVersion Version => SpectreAlgorithmVersion.v3;

        public SpectreUserKey_v3(string username, string userSecret) 
        {
            var scope = SpectreKeyPurpose.Authentication.ToPurposeScope();
            _logger.LogTrace("keyScope: {0}", scope);
            _logger.LogTrace("userKeySalt: keyScope={0} | #userName={1:X9} | userName={2}", scope, username.Length, username);
            using var userKeySalt = new MemoryStream();
            using var writer = new BinaryWriter(userKeySalt, Encoding.ASCII);
            userKeySalt.Write(Encoding.ASCII.GetBytes(scope));
            userKeySalt.Write(BitConverter.GetBytes(username.Length).Reverse().ToArray());
            userKeySalt.Write(Encoding.ASCII.GetBytes(username));
            var buffer = userKeySalt.ToArray();
            _logger.LogTrace(" => userKeySalt.id: {0}", Convert.ToHexString(SpectreUtil.HashSHA256(buffer)));
            _logger.LogTrace("userKey: scrypt(userSecret, userKeySalt, N={0}, R={1}, P={2})", ScryptN, ScryptR, ScryptP);
            _keyMaterial = new byte[ScryptDKLen];
            var scryptResult = Interop.crypto_pwhash_scryptsalsa208sha256_ll(Encoding.ASCII.GetBytes(userSecret), (nuint)userSecret.Length, buffer, (nuint)buffer.Length,
                ScryptN, ScryptR, ScryptP, _keyMaterial, (nuint)_keyMaterial.Length);
            if (scryptResult != 0)
            {
                var e = new ApplicationException($"Failed to scrypt ({scryptResult})");
                _logger.LogError(e, "Failed to scrypt!");
                throw e;
            }

            ID = SpectreUtil.StringSHA256(_keyMaterial);
            _logger.LogTrace(" => userKey.id: {0}", this);
        }

        public ISpectreSiteKey CreateSiteKey(string siteName, SpectreCounter keyCounter, SpectreKeyPurpose keyPurpose, string? keyContext)
            => new SpectreSiteKey_v3(_keyMaterial, siteName, keyCounter, keyPurpose, keyContext);

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
