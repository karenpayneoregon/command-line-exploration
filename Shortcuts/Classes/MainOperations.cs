namespace Shortcuts.Classes;

internal class MainOperations
{
    public static void DisplayShortcuts()
    {
        var (shortcuts, length) = FileOperations.Read();

        AnsiConsole.MarkupLine("  [cyan]ReSharper and Visual Studio[/]");
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