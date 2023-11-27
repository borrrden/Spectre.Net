using Spectre.Net.Api;

using System;
using System.Collections.Generic;
using System.Text;

namespace Spectre.Models
{
    public sealed class SiteTableEntry
    {
        private readonly int _algorithm = 3;
        private readonly DateTimeOffset _lastUsed;

        public string SiteName { get; }

        public int Counter { get; } = 1;

        public SpectreResultType ResultType { get; } = SpectreResultType.Long;

        public bool Exists { get; }

        public SiteTableEntry(SpectreSave spectreSave, string siteName)
        {
            SiteName = siteName;
            Exists = spectreSave.Sites.TryGetValue(siteName, out var site);
            if (Exists)
            {
                _algorithm = site!.Algorithm;
                _lastUsed = site.LastUsed;
                Counter = site.Counter;
                ResultType = (SpectreResultType)site.Type;
            }
        }

        public override string ToString()
        {
            return Exists ? $"{SiteName} ({_lastUsed.ToLocalTime():MMM dd, yyyy, hh:mm:ss tt} - V{_algorithm} - #{Counter})"
                : $"{SiteName} <Add new site>";
        }
    }
}
