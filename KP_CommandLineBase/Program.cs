using System.CommandLine;
using System.CommandLine.Builder;
using System.CommandLine.Parsing;
using KP_CommandLineBase.Classes;

namespace KP_CommandLineBase;

internal class Program
{
    static async Task Main(string[] args)
    {
        var firstNameOption = new Option<string>("--first")
        {
            Description = "First name",
            IsRequired = true
        };
        firstNameOption.AddAlias("-f");

        var lastNameOption = new Option<string>("--last")
        {
            Description = "last name",
            IsRequired = true
        };
        lastNameOption.AddAlias("-l");

        RootCommand rootCommand = new("Example for a basic command line tool")
        {
            firstNameOption,
            lastNameOption
        };

        rootCommand.SetHandler(MainOperations.MainCommand, firstNameOption, lastNameOption);

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
