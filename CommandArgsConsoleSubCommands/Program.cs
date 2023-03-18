using System.CommandLine;
using Spectre.Console;

namespace CommandArgsConsoleSubCommands;

class Program
{
    static int Main(string[] args)
    {
        var fileOption = new Option<FileInfo?>(
            name: "--file",
            description: "The file to read and display on the console.");

        var foreColorOption = new Option<bool>(
            name: "--light-mode",
            description: "Specifies foreground color");

        var rootCommand = new RootCommand("Sample app for System.CommandLine");

        var readCommand = new Command("read", "Read and display the file.")
            {
                fileOption,
                foreColorOption
            };
        rootCommand.AddCommand(readCommand);

        readCommand.SetHandler(async (file,  lightMode) =>
        {
            await ReadFile(file!, lightMode);
        }, fileOption, foreColorOption);

        return rootCommand.InvokeAsync(args).Result;
    }

    internal static Task ReadFile(FileInfo file,  bool lightMode)
    {
        AnsiConsole.Foreground = lightMode ? Color.Silver : Color.DodgerBlue1;
        
        if (File.Exists(file.FullName))
        {
            List<string> lines = File.ReadLines(file.FullName).ToList();

            IEnumerable<string[]> chunks = lines.Chunk(10);

            foreach (var chunk in chunks)
            {
                AnsiConsole.Write(string.Join("\n", chunk));
                Console.WriteLine();
            }
        }
        else
        {
            AnsiConsole.MarkupLine($"[red]Not found[/] [cyan]{file.FullName}[/]");
        }

        return Task.CompletedTask;

    }
}
