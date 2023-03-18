using System.CommandLine;
using System.CommandLine.Builder;
using System.CommandLine.Parsing;
using CommandArgsConsoleApp2.Classes;

namespace CommandArgsConsoleApp2;

internal class Program
{
    static async Task Main(string[] args)
    {

        var environmentOption = new Option<Classes.Environment>("--environment")
        {
            Description = "Application runtime environment",
            IsRequired = true
        };
        environmentOption.AddAlias("-e");

        var userNameOption = new Option<string>("--username")
        {
            Description = "Current user name",
            IsRequired = false
        };
        userNameOption.AddAlias("-u");

        var logOption = new Option<bool>("--log")
        {
            Description = "Use SeriLog",
            IsRequired = true
        };
        logOption.AddAlias("-l");


        // --environment Development --username "karen payne" --log true

        RootCommand rootCommand = new("Environment sample")
        {
            environmentOption,
            userNameOption,
            logOption
        };

        rootCommand.SetHandler(MainOperations.MainCommand, environmentOption, userNameOption, logOption);

        var commandLineBuilder = new CommandLineBuilder(rootCommand);

        commandLineBuilder.AddMiddleware(async (context, next) =>
        {
            await next(context);
        });

        commandLineBuilder.UseDefaults();
        var parser = commandLineBuilder.Build();
        await parser.InvokeAsync(args);

    }
}