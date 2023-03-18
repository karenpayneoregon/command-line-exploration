using System.CommandLine;
using System.CommandLine.Builder;
using System.CommandLine.Help;
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
        Parser parser = commandLineBuilder.Build();
        

        //var parser = new CommandLineBuilder(rootCommand)
        //    .UseDefaults()
        //    .UseHelp(ctx =>
        //    {
        //        ctx.HelpBuilder.CustomizeSymbol(environmentOption,
        //            firstColumnText: "--color <Black, White, Red, or Yellow>",
        //            secondColumnText: "Specifies the foreground color. " +
        //                              "Choose a color that provides enough contrast " +
        //                              "with the background color. " +
        //                              "For example, a yellow foreground can't be read " +
        //                              "against a light mode background.");
        //        ctx.HelpBuilder.CustomizeLayout(
        //            _ =>
        //                HelpBuilder.Default
        //                    .GetLayout()
        //                    .Skip(1) // Skip the default command description section.
        //                    .Prepend(
        //                        _ => Spectre.Console.AnsiConsole.Write(
        //                            new FigletText(rootCommand.Description!))
        //                    ));
        //    })
        //    .Build();

        await parser.InvokeAsync(args);
        Console.ReadLine();
        //--environment Development --username "karen payne" --log true
    }
}