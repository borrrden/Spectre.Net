﻿using Microsoft.Extensions.Logging;

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
        AnsiConsole.Write(new Markup($"[bold red]{siteKey.CreatePassword((SpectreResultType)settings.Type)}[/]"));

        return 0;
    }

    public sealed class Settings : CommandSettings
    {
        [Description("The spectre username to generate the password for")]
        [CommandOption("-u|--username")]
        public string? Username { get; init; }

        [Description("The site name to generate the password for")]
        [CommandArgument(0, "<site>")]
        [Required]
        public string Site { get; init; } = "";

        [Description("The type of password to generate")]
        [CommandOption("-t|--type")]
        [DefaultValue((PasswordType)17)]
        public PasswordType Type { get; init; } = PasswordType.Long;

        [Description("The counter to use when generating the password")]
        [CommandOption("-c|--counter")]
        [DefaultValue(1)]
        public int Counter { get; init; } = 1;

        [Description("Enables library trace output")]
        [CommandOption("--trace")]
        [DefaultValue(false)]
        public bool Trace { get; init; }
    }
}

internal sealed class PasswordTypeConverter : TypeConverter
{
    public override object? ConvertFrom(ITypeDescriptorContext? context, CultureInfo? culture, object value)
    {
        if (value is int intValue)
        {
            return (PasswordType)intValue;
        }

        return base.ConvertFrom(context, culture, value);
    }

    public override object? ConvertTo(ITypeDescriptorContext? context, CultureInfo? culture, object? value, Type destinationType)
    {
        if (value is PasswordType pass)
        {
            if (destinationType == typeof(Int32))
            {
                return (int)pass;
            }
        }

        return base.ConvertTo(context, culture, value, destinationType);
    }
}

[TypeConverter(typeof(PasswordTypeConverter))]
enum PasswordType
{
    Basic = (int)SpectreResultType.Basic,
    Short = (int)SpectreResultType.Short,
    Medium = (int)SpectreResultType.Medium,
    Long = (int)SpectreResultType.Long,
    Maximum = (int)SpectreResultType.Maximum
}
