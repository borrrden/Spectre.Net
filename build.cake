var target = Argument("target", "Build");
var configuration = Argument("configuration", "Release");

var projectList = new[] { "Spectre.Net.Api", "Spectre.Cli", "Spectre.Net", "Spectre.Linux" };

Task("Clean")
    .Does(context => 
{
    context.CleanDirectory("./artifacts");
    foreach(var proj in projectList) {
        DotNetClean($"{proj}/{proj}.csproj", new DotNetCleanSettings {
            Configuration = configuration,
            Verbosity = DotNetVerbosity.Minimal,
            NoLogo = true,
        });
    }
});

Task("Build")
    .IsDependentOn("Clean")
    .Does(context => 
{
    foreach(var proj in projectList)
    {
        Information($"Compiling {proj}");
        DotNetBuild($"{proj}/{proj}.csproj", new DotNetBuildSettings {
            Configuration = configuration,
            Verbosity = DotNetVerbosity.Minimal,
            NoLogo = true,
            NoIncremental = context.HasArgument("rebuild"),
            MSBuildSettings = new DotNetMSBuildSettings()
                .TreatAllWarningsAs(MSBuildTreatAllWarningsAs.Error)
        });
    }
});

RunTarget(target);