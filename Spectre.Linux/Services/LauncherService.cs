﻿using Spectre.Services;

using System;
using System.Threading.Tasks;

namespace Spectre.Linux.Services
{
    internal sealed class LauncherService : ILauncherService
    {
        public Task OpenAsync(string url)
        {
            throw new NotImplementedException();
        }
    }
}