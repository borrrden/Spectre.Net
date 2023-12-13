// -----------------------------------------------------------------------
// <copyright file="Program.cs" company="Jim Borden">
// Copyright (c) Jim Borden. All rights reserved.
// Licensed under the GPL-3.0 license. See LICENSE.md file in the project root for full license information.
// </copyright>
// -----------------------------------------------------------------------

using UIKit;

namespace Spectre.Net;

/// <summary>
/// The top level entry point for the iOS application.
/// </summary>
public class Program
{
    /// <summary>
    /// The top level main function.
    /// </summary>
    /// <param name="args">Command line arguments.</param>
    public static void Main(string[] args)
    {
        // if you want to use a different Application Delegate class from "AppDelegate"
        // you can specify it here.
        UIApplication.Main(args, null, typeof(AppDelegate));
    }
}