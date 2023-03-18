using System.CommandLine;
using System.CommandLine.Builder;
using System.CommandLine.Help;
using System.CommandLine.Parsing;
using CommandArgsConsoleAppHelp.Classes;
using Spectre.Console;

namespace CommandArgsConsoleAppHelp;

internal class Program
{
    static async Task Main(string[] args)
    {

        /*
         *
         */
        var userNameOption = new Option<string>("--username")
        {
            Description = "Current user name",
            IsRequired = true
        };
        userNameOption.AddAlias("-u");


        // --environment Development --username "karen payne" --log true

        RootCommand rootCommand = new("Get user name")
        {
            userNameOption
        };

        rootCommand.SetHandler(MainOperations.MainCommand,  userNameOption);

        var commandLineBuilder = new CommandLineBuilder(rootCommand);

        commandLineBuilder.AddMiddleware(async (context, next) =>
        {
            await next(context);
        });
        
        var parser = new CommandLineBuilder(rootCommand)
            .UseDefaults()
            .UseHelp(ctx =>
            {
                ctx.HelpBuilder.CustomizeSymbol(userNameOption,
                    firstColumnText: "--username ",
                    secondColumnText: "If entering first and last name use double quotes\notherwise enter your first name");
                ctx.HelpBuilder.CustomizeLayout(
                    _ =>
                        HelpBuilder.Default
                            .GetLayout()
                            .Skip(1) // Skip the default command description section.
                            // add header
                            .Prepend(_ => AnsiConsole.Write(
                                    new Panel($"[cyan]{rootCommand.Description!}[/] code sample")
                                        .Header("[green1]Help[/]")))
                            // add text after help
                            .Append(_ => AnsiConsole.MarkupLine("[white]Some footer[/]")));
            })
            .Build();
     
        await parser.InvokeAsync(args);
     
    }


}