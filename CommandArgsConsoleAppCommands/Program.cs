using System.CommandLine;
using System.CommandLine.Builder;
using System.CommandLine.Parsing;

namespace CommandArgsConsoleAppCommands;

internal class Program
{
    static async Task Main(string[] args)
    {
        RootCommand rootCommand = new RootCommand();
        var sub1Command = new Command("sub1", "First-level subcommand");
        
        rootCommand.Add(sub1Command);
        var sub1aCommand = new Command("sub1a", "Second level subcommand");
        sub1Command.Add(sub1aCommand);

        var commandLineBuilder = new CommandLineBuilder(rootCommand);
        commandLineBuilder.AddMiddleware(async (context, next) =>
        {
            await next(context);
        });

        commandLineBuilder.UseDefaults();
        Parser parser = commandLineBuilder.Build();
        await parser.InvokeAsync(args);
        Console.ReadLine();

    }
}
