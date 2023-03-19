using Holidays.Classes;
using System.CommandLine;
using System.CommandLine.Builder;
using System.CommandLine.Parsing;


namespace Holidays;

internal partial class Program
{
    static async Task Main(string[] args)
    {
        var environmentOption = new Option<string>("--code")
        {
            Description = "Two letter country code",
            IsRequired = true
        };
        environmentOption.AddAlias("-c");

        RootCommand rootCommand = new("Get holidays for country in current year")
        {
            environmentOption
        };

        rootCommand.SetHandler(MainOperations.Run, environmentOption);
            
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