namespace CommandArgsConsoleApp2.Classes;
internal class MainOperations
{
    public static void MainCommand(Environment currentEnvironment, string userName, bool log)
    {
        AnsiConsole.MarkupLine($"[yellow]environment[/] {currentEnvironment}");
        AnsiConsole.MarkupLine($"        [yellow]log[/] {log}");

        if (userName is not null)
        {
            AnsiConsole.MarkupLine($"[yellow]   username[/] {userName}");
        }

        if (log)
        {
            SetupLogging.Initialize(currentEnvironment);
            LogOperations.CreateSomeLogs();
        }
    }
}
