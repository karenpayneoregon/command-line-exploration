using Spectre.Console;

namespace CommandArgsConsoleAppHelp.Classes;

public class MainOperations
{
    public static void MainCommand(string userName)
    {
        AnsiConsole.MarkupLine($"User name [white]{userName}[/]");
    }
}