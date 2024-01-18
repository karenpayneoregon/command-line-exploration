using Spectre.Console;
using SqlServerIndices.Models;

namespace SqlServerIndices.Classes;

public class Worker
{
    /// <summary>
    /// * Present a menu of database
    ///     * Traverse database tables
    ///     * If at least one column in a table has a description add to list else bypass it
    ///     * Display details
    /// </summary>
    public static void Execute()
    {
        DatabaseName databaseName = new DatabaseName() { Id = 0 };

        while (databaseName.Id != -1)
        {
            AnsiConsole.Clear();

            databaseName = AnsiConsole.Prompt(MenuOperations.DatabaseNamesMenu());

            if (databaseName.Id == -1)
            {
                return;
            }

            DataOperations.GetIndexInformation(databaseName.Name);
            AnsiConsole.MarkupLine("Press [cyan]Enter[/] for menu");
            Console.ReadLine();
            
        }
    }
}
