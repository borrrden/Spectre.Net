// -----------------------------------------------------------------------
// <copyright file="SpectreTypes.cs" company="Jim Borden">
// Copyright (c) Jim Borden. All rights reserved.
// Licensed under the GPL-3.0 license. See LICENSE.md file in the project root for full license information.
// </copyright>
// -----------------------------------------------------------------------

using Microsoft.Extensions.Logging;

namespace Spectre.Net.Api;

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

    public static string GetTemplate(SpectreResultType type, int templateIndex)
    {
        var templates = GetTemplates(type);
        return templates[templateIndex % templates.Count];
    }

    public static char GetClassCharacter(char characterClass, byte seedByte)
    {
        var classCharacters = GetClassCharacters(characterClass);
        return classCharacters[seedByte % classCharacters.Length];
    }

    private static string GetClassCharacters(char characterClass)
    {
        switch (characterClass) {
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
                _logger.LogWarning(e, "Unknown character class");
                throw e;
        }
    }

    private static IReadOnlyList<string> GetTemplates(SpectreResultType type)
    {
        if (!type.HasFlag(SpectreResultType.TemplateClass)) {
            var e = new ArgumentException($"Not a generated type: {type}", nameof(type));
            _logger.LogWarning(e, "Not a generated type");
            throw e;
        }

        switch (type) {
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
                _logger.LogWarning(e, "Unknown generated type");
                throw e;
        }
    }
}