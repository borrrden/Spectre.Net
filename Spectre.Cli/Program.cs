using Spectre.Cli;
using Spectre.Console.Cli;

using System.Diagnostics.CodeAnalysis;

using RootCommand = Spectre.Cli.RootCommmand;

internal class Program
{

    [DynamicDependency(DynamicallyAccessedMemberTypes.All, typeof(RootCommand))]
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