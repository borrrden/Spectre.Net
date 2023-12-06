using Microsoft.Extensions.Logging;

using Spectre.Console;
using Spectre.Console.Cli;
using Spectre.Net.Api;

using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;
using System.Globalization;

using RootCommand = Spectre.Cli.RootCommmand;

namespace Spectre.Cli;


internal sealed class RootCommmand : Command<RootCommand.Settings>
{
    public override int Execute(CommandContext context, Settings settings)
    {
        if (settings.Trace)
        {
            SpectreUtil.LoggerFactory = LoggerFactory.Create(static builder =>
            {
                builder.AddConsole().AddDebug().SetMinimumLevel(LogLevel.Trace);
            });
        }

        AnsiConsole.Clear();
        AnsiConsole.Write(new FigletText("Spectre").Color(Color.DarkGreen));
        AnsiConsole.WriteLine();
        var username = settings.Username;
        while (username == null)
        {
            username = AnsiConsole.Ask<string>("Enter Spectre [green]username[/]:");
        }

        var userSecret = AnsiConsole.Prompt(
            new TextPrompt<string>("Enter [green]password[/]:").Secret());
        var userKey = SpectreAlgorithm.CreateUserKey(username, userSecret, SpectreAlgorithmVersion.v3);
        var siteKey = userKey.CreateSiteKey(settings.Site, (SpectreCounter)settings.Counter, SpectreKeyPurpose.Authentication, null);
        AnsiConsole.WriteLine();
        AnsiConsole.Write(new Markup($"[bold red]{siteKey.CreatePassword(Enum.Parse<SpectreResultType>(settings.Type, true))}[/]"));

        return 0;
    }

    public sealed class Settings : CommandSettings
    {
        private static readonly string[] AllowedTypeValues = ["maximum", "long", "medium", "short", "basic", "pin"];

        [Description("The spectre username to generate the password for")]
        [CommandOption("-u|--username")]
        public string? Username { get; init; }

        [Description("The site name to generate the password for")]
        [CommandArgument(0, "<site>")]
        [Required]
        public string Site { get; init; } = "";

        [Description("The type of password to generate")]
        [CommandOption("-t|--type")]
        [DefaultValue("Long")]
        public string Type { get; init; } = "Long";

        [Description("The counter to use when generating the password")]
        [CommandOption("-c|--counter")]
        [DefaultValue(1)]
        public int Counter { get; init; } = 1;

        [Description("Enables library trace output")]
        [CommandOption("--trace")]
        [DefaultValue(false)]
        public bool Trace { get; init; }

        public override Console.ValidationResult Validate()
        {
            if (!AllowedTypeValues.Contains(Type.ToLower()))
            {
                return Console.ValidationResult.Error("Type must be one of Maximum, Long, Medium, Short, Basic, PIN");
            }

            return Console.ValidationResult.Success();
        }
    }
}
