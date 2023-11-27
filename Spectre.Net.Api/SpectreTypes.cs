using Microsoft.Extensions.Logging;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace Spectre.Net.Api
{
    public enum SpectreAlgorithmVersion : uint
    {
        /** (2012-03-05) V0 incorrectly performed host-endian math with bytes translated into 16-bit network-endian. */
        v0,
        /** (2012-07-17) V1 incorrectly sized site name fields by character count rather than byte count. */
        v1,
        /** (2014-09-24) V2 incorrectly sized user name fields by character count rather than byte count. */
        v2,
        /** (2015-01-15) V3 is the current version. */
        v3,
        Current = v3,
        First = v0,
        Last = v3
    }

    public enum SpectreResultType : uint
    {
        /** 0: Don't produce a result */
        None = 0,
        /** Use the site key to generate a result from a template. */
        TemplateClass = 1 << 4,
        /** Use the site key to encrypt and decrypt a stateful entity. */
        StatefulClass = 1 << 5,
        /** Use the site key to derive a site-specific object. */
        DeriveClass = 1 << 6,
        /** Export the key-protected content data. */
        ExportContentFeature = 1 << 10,
        /** Never export content. */
        DevicePrivateFeature = 1 << 11,
        /** Don't use this as the primary authentication result type. */
        AlternateFeature = 1 << 12,
        /** 16: pg^VMAUBk5x3p%HP%i4= */
        Maximum = 0x0 | TemplateClass | None,
        /** 17: BiroYena8:Kixa */
        Long = 0x1 | TemplateClass | None,
        /** 18: BirSuj0- */
        Medium = 0x2 | TemplateClass | None,
        /** 19: Bir8 */
        Short = 0x3 | TemplateClass | None,
        /** 20: pO98MoD0 */
        Basic = 0x4 | TemplateClass | None,
        /** 21: 2798 */
        PIN = 0x5 | TemplateClass | None,
        /** 30: birsujano */
        Name = 0xE | TemplateClass | None,
        /** 31: bir yennoquce fefi */
        Phrase = 0xF | TemplateClass | None,

        /** 1056: Custom saved result. */
        Personal = 0x0 | StatefulClass | ExportContentFeature,
        /** 2081: Custom saved result that should not be exported from the device. */
        Device = 0x1 | StatefulClass | DevicePrivateFeature,

        /** 4160: Derive a unique binary key. */
        DeriveKey = 0x0 | DeriveClass | AlternateFeature,

        DefaultResult = Long,
        DefaultLogin = Name
    }

    public enum SpectreKeyPurpose : byte
    {
        /** Generate a key for authentication. */
        Authentication,
        /** Generate a name for identification. */
        Identification,
        /** Generate a recovery token. */
        Recovery
    }

    public enum SpectreCounter : uint
    {
        /** Use a time-based counter value, resulting in a TOTP generator. */
        TOTP = 0,
        /** The initial value for a site's counter. */
        Initial = 1,

        Default = Initial,
        First = TOTP,
        Last = UInt32.MaxValue
    }

    public static class EnumExtensions
    {
        public static string ToPurposeScope(this SpectreKeyPurpose purpose)
        {
            switch (purpose)
            {
                case SpectreKeyPurpose.Authentication:
                    return "com.lyndir.masterpassword";
                case SpectreKeyPurpose.Identification:
                    return "com.lyndir.masterpassword.login";
                case SpectreKeyPurpose.Recovery:
                    return "com.lyndir.masterpassword.answer";
                default:
                    throw new ArgumentException($"Unknown purpose {purpose}");
            }
        }
    }

    public static class SpectreType
    {
        private static readonly ILogger _logger = SpectreUtil.LoggerFactory.CreateLogger("SpectreType");

        private static readonly IReadOnlyList<string> MaximumList = new List<string> { "anoxxxxxxxxxxxxxxxxx", "axxxxxxxxxxxxxxxxxno" };
        private static readonly IReadOnlyList<string> LongList = new List<string> { 
                    "CvcvnoCvcvCvcv", "CvcvCvcvnoCvcv", "CvcvCvcvCvcvno",
                    "CvccnoCvcvCvcv", "CvccCvcvnoCvcv", "CvccCvcvCvcvno",
                    "CvcvnoCvccCvcv", "CvcvCvccnoCvcv", "CvcvCvccCvcvno",
                    "CvcvnoCvcvCvcc", "CvcvCvcvnoCvcc", "CvcvCvcvCvccno",
                    "CvccnoCvccCvcv", "CvccCvccnoCvcv", "CvccCvccCvcvno",
                    "CvcvnoCvccCvcc", "CvcvCvccnoCvcc", "CvcvCvccCvccno",
                    "CvccnoCvcvCvcc", "CvccCvcvnoCvcc", "CvccCvcvCvccno"
        };
        private static readonly IReadOnlyList<string> MediumList = new List<string> { "CvcnoCvc", "CvcCvcno" };
        private static readonly IReadOnlyList<string> ShortList = new List<string> { "Cvcn" };
        private static readonly IReadOnlyList<string> BasicList = new List<string> { "aaanaaan", "aannaaan", "aaannaaa" };
        private static readonly IReadOnlyList<string> PINList = new List<string> { "nnnn" };
        private static readonly IReadOnlyList<string> NameList = new List<string> { "cvccvcvcv" };
        private static readonly IReadOnlyList<string> PhraseList = new List<string> { "cvcc cvc cvccvcv cvc", "cvc cvccvcvcv cvcv", "cv cvccv cvc cvcvccv" };

        private static IReadOnlyList<string> GetTemplates(SpectreResultType type)
        {
            if (!type.HasFlag(SpectreResultType.TemplateClass))
            {
                var e = new ArgumentException($"Not a generated type: {type}", nameof(type));
                _logger.LogWarning(e.Message);
                throw e;
            }

            switch (type)
            {
                case SpectreResultType.Maximum:
                    return MaximumList;
                case SpectreResultType.Medium:
                    return MediumList;
                case SpectreResultType.Long:
                    return LongList;
                case SpectreResultType.Short:
                    return ShortList;
                case SpectreResultType.Basic:
                    return BasicList;
                case SpectreResultType.PIN:
                    return PINList;
                case SpectreResultType.Name:
                    return NameList;
                case SpectreResultType.Phrase:
                    return PhraseList;
                default:
                    var e = new ArgumentException($"Unknown generated type: {type}", nameof(type));
                    _logger.LogWarning(e.Message);
                    throw e;
            }
        }

        public static string GetTemplate(SpectreResultType type, int templateIndex)
        {
            var templates = GetTemplates(type);
            return templates[templateIndex % templates.Count];
        }

        private static string GetClassCharacters(char characterClass)
        {
            switch (characterClass)
            {
                case 'V':
                    return "AEIOU";
                case 'C':
                    return "BCDFGHJKLMNPQRSTVWXYZ";
                case 'v':
                    return "aeiou";
                case 'c':
                    return "bcdfghjklmnpqrstvwxyz";
                case 'A':
                    return "AEIOUBCDFGHJKLMNPQRSTVWXYZ";
                case 'a':
                    return "AEIOUaeiouBCDFGHJKLMNPQRSTVWXYZbcdfghjklmnpqrstvwxyz";
                case 'n':
                    return "0123456789";
                case 'o':
                    return "@&%?,=[]_:-+*$#!'^~;()/.";
                case 'x':
                    return "AEIOUaeiouBCDFGHJKLMNPQRSTVWXYZbcdfghjklmnpqrstvwxyz0123456789!@#$%^&*()";
                case ' ':
                    return " ";
                default:
                    var e = new ArgumentException($"Unknown character class {characterClass}", nameof(characterClass));
                    _logger.LogWarning(e.Message);
                    throw e;

            }
        }

        public static char GetClassCharacter(char characterClass, byte seedByte)
        {
            var classCharacters = GetClassCharacters(characterClass);
            return classCharacters[seedByte % classCharacters.Length];
        }
    }
}
