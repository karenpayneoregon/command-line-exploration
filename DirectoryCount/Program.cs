using System.CommandLine;
using System.CommandLine.Builder;
using System.CommandLine.Parsing;
using DirectoryCount.Classes;

namespace DirectoryCount;

internal class Program
{
    static async Task Main(string[] args)
    {
    
        var directoryOption = new Option<string>("--dir")
        {
            Description = "Directory to get details for",
            IsRequired = true
        };
        directoryOption.AddAlias("-d");

        RootCommand rootCommand = new("Get folder and file counts recursively")
        {
            directoryOption
        };

        rootCommand.SetHandler(MainOperations.Run, directoryOption);

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
