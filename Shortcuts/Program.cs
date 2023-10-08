using System.CommandLine;
using System.Data;
using System.CommandLine.Builder;
using System.CommandLine.Parsing;
using Shortcuts.Classes;

namespace Shortcuts;

internal partial class Program
{
    static async Task Main(string[] args)
    {
        RootCommand rootCommand = new("Visual Studio shortcuts");
        rootCommand.SetHandler(MainOperations.DisplayShortcuts);

        var commandLineBuilder = new CommandLineBuilder(rootCommand);

        commandLineBuilder.AddMiddleware(async (context, next) =>
        {
            await next(context);
        });

        commandLineBuilder.UseDefaults();
        Parser parser = commandLineBuilder.Build();

        await parser.InvokeAsync(args);

    }
}