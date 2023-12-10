// -----------------------------------------------------------------------
// <copyright file="SiteNameAndData.cs" company="Jim Borden">
// Copyright (c) Jim Borden. All rights reserved.
// Licensed under the GPL-3.0 license. See LICENSE.md file in the project root for full license information.
// </copyright>
// -----------------------------------------------------------------------

using Spectre.Net.Api;

namespace Spectre.ViewModels;

/// <summary>
/// A record containing a site's name and data for inclusion in the main page list.
/// </summary>
/// <param name="SiteName">The name of the site (e.g. duckduckgo.com).</param>
/// <param name="SiteData">The settings for generating the password for the site.</param>
public readonly record struct SiteNameAndData(string SiteName, SiteData SiteData);