using Spectre.Console;
using Spectre.Console.Cli;
using Spectre.Console.Json;
using Spectre.Net.Api;

using System.ComponentModel;

namespace Spectre.Cli;

// Just because I wanted an excuse to use JsonText
internal sealed class DumpCommand : Command<DumpCommand.Settings>
{
    private readonly ISpectreUserManager _userManager = new SpectreUserManager();

    public override int Execute(CommandContext context, Settings settings)
    {
        AnsiConsole.Clear();
        var userData = _userManager.ReadUserRaw(settings.Name);
        if (userData == null)
        {
            AnsiConsole.MarkupLine($"[red]User {settings.Name} not found![/]");
            return 1;
        }

        AnsiConsole.Write(new JsonText(userData));

        return 0;
    }

    public sealed class Settings : CommandSettings
    {
        [Description("The username to dump")]
        [CommandArgument(0, "<name>")]
        public string Name { get; init; } = "";
    }
}
