// -----------------------------------------------------------------------
// <copyright file="Program.cs" company="Jim Borden">
// Copyright (c) Jim Borden. All rights reserved.
// Licensed under the GPL-3.0 license. See LICENSE.md file in the project root for full license information.
// </copyright>
// -----------------------------------------------------------------------

using System.Diagnostics.CodeAnalysis;
using Spectre.Cli;
using Spectre.Console.Cli;
using RootCommand = Spectre.Cli.RootCommand;

internal class Program
{
    [DynamicDependency(DynamicallyAccessedMemberTypes.All, typeof(RootCommand))]
    [DynamicDependency(DynamicallyAccessedMemberTypes.All, typeof(RootCommand.Settings))]
    [DynamicDependency(DynamicallyAccessedMemberTypes.All, typeof(DumpCommand))]
    [DynamicDependency(DynamicallyAccessedMemberTypes.All, "Spectre.Console.Cli.ExplainCommand", "Spectre.Console.Cli")]
    [DynamicDependency(DynamicallyAccessedMemberTypes.All, "Spectre.Console.Cli.XmlDocCommand", "Spectre.Console.Cli")]
    [DynamicDependency(DynamicallyAccessedMemberTypes.All, "Spectre.Console.Cli.VersionCommand", "Spectre.Console.Cli")]
    private static async Task<int> Main(string[] args)
    {
        var app = new CommandApp<RootCommand>();
        app.Configure(config =>
        {
            config.AddCommand<DumpCommand>("dump");
        });

        return await app.RunAsync(args);
    }
}