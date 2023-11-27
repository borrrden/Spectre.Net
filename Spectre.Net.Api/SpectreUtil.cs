using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Spectre.Net.Api
{
    public static class SpectreUtil
    {
        private static ILoggerFactory? _loggerFactory;
        
        public static ILoggerFactory LoggerFactory
        {
            get
            {
                if (_loggerFactory == null)
                {
                    _loggerFactory = Microsoft.Extensions.Logging.LoggerFactory.Create(static builder =>
                    {
                        builder.AddConsole().AddDebug().SetMinimumLevel(LogLevel.Warning);
                    });
                }

                return _loggerFactory;
            }
            set => _loggerFactory = value;
        }

        public static byte[] HashSHA256(string input)
        {
            return HashSHA256(Encoding.UTF8.GetBytes(input));
        }

        public static byte[] HashSHA256(byte[] input)
        {
            using var hasher = SHA256.Create();
            return hasher.ComputeHash(input);
        }

        public static string StringSHA256(string input)
        {
            return Convert.ToHexString(HashSHA256(input));
        }

        public static string StringSHA256(byte[] input)
        {
            return Convert.ToHexString(HashSHA256(input));
        }
    }
}
