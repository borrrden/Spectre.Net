using Spectre.Cli;
using Spectre.Console.Cli;

using RootCommand = Spectre.Cli.RootCommmand;

var app = new CommandApp<RootCommand>();
app.Configure(config =>
{
    config.AddCommand<DumpCommand>("dump");
    config.Settings.ShowOptionDefaultValues = false; // AOT doesn't print enums well and I don't like it printing '17' instead
});

return await app.RunAsync(args);