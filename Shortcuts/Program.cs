using System.CommandLine;
using Shortcuts.Classes;
using System.Data;
using System.CommandLine.Builder;
using System.CommandLine.Parsing;

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

internal class MainOperations
{
    public static void DisplayShortcuts()
    {
        //InitialOperations.CreateJsonFile();
        var (shortcuts, length) = FileOperations.Read();

        AnsiConsole.MarkupLine("  [cyan]ReSharper[/]");
        var table = new Table();
        table.AddColumn("[b]Description[/]").Alignment(Justify.Right);
        table.AddColumn(new TableColumn("[b]Shortcut[/]")).Alignment(Justify.Left);

        foreach (var root in shortcuts)
        {
            table.AddRow(root.Text(length), root.Combination());
        }

        AnsiConsole.Write(table);
    }
}