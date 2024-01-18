using Dapper;
using Microsoft.Data.SqlClient;
using Spectre.Console;
using SqlServerIndices.Models;

namespace SqlServerIndices.Classes;


internal class DataOperations
{
    public static string Server { get; set; } = Configuration.ServerSettings().Name;



    public static List<DatabaseName> DatabaseNamesDapper()
    {
        using SqlConnection cn = new($"Data Source={Server};Initial Catalog=master;integrated security=True;Encrypt=False");
        var list = cn.Query<DatabaseName>(SqlStatements.GetDatabaseNames).ToList();
        for (int index = 0; index < list.Count; index++)
        {
            list[index].Id = index +1;
        }

        list.Add(new DatabaseName() { Id = -1, Name = "Exit" });
        return list;
    }

    public static void GetIndexInformation(string databaseName)
    {
        using SqlConnection cn = new($"Data Source={Server};Initial Catalog={databaseName};integrated security=True;Encrypt=False");
        var list = cn.Query<Models.Container>(SqlStatements.GetIndices).ToList();

        List<GroupContainer> query = list.GroupBy(x => x.TableName)
            .Select(group => new GroupContainer(group.Key, group.OrderBy(x => x.ColumnName)))
            .OrderBy(group => group.TableName)
            .ThenBy(x => x.containerList.FirstOrDefault()!.ColumnName)
            .ToList();

        if (query.Any())
        {
            var table = CreateTable(databaseName);
            foreach (var container in query)
            {
                table.AddRow($"[white][u]{container.TableName}[/][/]");
                foreach (var container1 in container.containerList)
                {
                    table.AddRow("", container1.IndexId.ToString(), container1.ColumnName, container1.IndexName);
                }
            }
            AnsiConsole.Write(table);
        }
        else
        {
            AnsiConsole.MarkupLine($"[white on red]No indexes for {databaseName}[/]");
        }

    }

    private static Table CreateTable(string name)
    {
        var table = new Table()
            .AddColumn("Table name")
            .AddColumn("Column index")
            .AddColumn("Column name")
            .AddColumn("Index name")
            .Alignment(Justify.Left)
            .Title($"[cyan]{name}[/]")
            .BorderColor(Color.Grey);
        return table;
    }
}