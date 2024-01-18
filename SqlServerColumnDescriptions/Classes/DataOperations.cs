using Microsoft.Data.SqlClient;
using System.Data;
using SqlServerColumnDescriptions.Models;
using Dapper;


namespace SqlServerColumnDescriptions.Classes;

/// <summary>
/// Provides access to read database table column descriptions if present.
/// Hard coded to SQLEXPRESS.
/// </summary>
/// <remarks>
/// Originally done conventional (still here) than ported to using Dapper
/// </remarks>
internal class DataOperations
{
    public static string Server { get; set; } = ".\\SQLEXPRESS";

    public static List<DatabaseName> DatabaseNames()
    {
        List<DatabaseName> list = new();
        int identifier = 1;
        using SqlConnection cn = new($"Data Source={Server};Initial Catalog=master;integrated security=True;Encrypt=False");
        using SqlCommand cmd = new() { Connection = cn, CommandText = SqlStatements.GetDatabaseNames};
        cn.Open();
        using var reader =  cmd.ExecuteReader();

        while (reader.Read())
        {
            list.Add(new DatabaseName() {Name = reader.GetString(0), Id = identifier});
            identifier++;
        }

        list.Add(new DatabaseName() {Id = -1, Name = "Exit"});
        return list;
    }

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

    public static (List<DatabaseTable> list, Exception exception) GetDetails(string databaseName)
    {
        List<DatabaseTable> list = new ();

        try
        {
            using SqlConnection cn = new($"Data Source={Server};Initial Catalog={databaseName};integrated security=True;Encrypt=False");
            using SqlCommand cmd = new() { Connection = cn, CommandText = SqlStatements.GetTableNames(databaseName) };
            cn.Open();
            using var reader = cmd.ExecuteReader();

            List<string> tableNames = new();
            while (reader.Read())
            {
                tableNames.Add(reader.GetString(0).Replace("dbo.", ""));
            }
            
            return (GetDescriptionsDapper(databaseName, tableNames),null)!;
        }
        catch (Exception exception)
        {
            return (list, exception);
        }
    }

    public static (List<DatabaseTable> list, Exception exception) GetDetailsDapper(string databaseName)
    {
        List<DatabaseTable> dataList = new();

        try
        {
            using SqlConnection cn = new($"Data Source={Server};Initial Catalog={databaseName};integrated security=True;Encrypt=False");
            var list = cn.Query<string>(SqlStatements.GetTableNames(databaseName));
            List<string> tableNames = new();
            // if so desire, modify the SQL above to exclude dbo. so the following is not needed.
            foreach (var item in list)
            {
                tableNames.Add(item.Replace("dbo.", ""));
            }
            
            return (GetDescriptionsDapper(databaseName, tableNames), null)!;
        }
        catch (Exception exception)
        {
            return (dataList, exception);
        }
    }

    /// <summary>
    /// Get column descriptions for a table
    /// </summary>
    /// <param name="databaseName">catalog</param>
    /// <param name="tableNames">table names in catalog</param>
    /// <remarks>
    /// Being optimistic that we can dispense with exception handling since we just open the connection in the calling procedure
    /// </remarks>
    public static List<DatabaseTable> GetDescriptions(string databaseName,List<string> tableNames)
    {
        List<DatabaseTable> list = new();
        using SqlConnection cn = new($"Data Source={Server};Initial Catalog={databaseName};integrated security=True;Encrypt=False");
        using SqlCommand cmd = new() { Connection = cn, CommandText = SqlStatements.Descriptions() };
        cmd.Parameters.Add("@TableName", SqlDbType.NChar);
        cn.Open();

        foreach (var tableName in tableNames)
        {
                
            cmd.Parameters["@TableName"].Value = tableName;
            var reader = cmd.ExecuteReader();

            if (reader.HasRows)
            {
                DatabaseTable item = new() {DatabaseName = databaseName, TableName = tableName, ColumnsList = new List<Columns>()};
                while (reader.Read())
                {
                    item.ColumnsList.Add(new Columns() {Name = reader.GetString(0), Description = reader.GetString(2)});
                }
                list.Add(item);
            }
            reader.Close();
        }
        return list;
    }
    public static List<DatabaseTable> GetDescriptionsDapper(string databaseName, List<string> tableNames)
    {
        List<DatabaseTable> list = new();
        using SqlConnection cn = new($"Data Source={Server};Initial Catalog={databaseName};integrated security=True;Encrypt=False");

        foreach (var tableName in tableNames)
        {
            var descriptions = cn.Query<DescriptionContainer>(SqlStatements.Descriptions(), new { TableName = tableName });
            if (!descriptions.Any()) continue;
            DatabaseTable item = new() { DatabaseName = databaseName, TableName = tableName, ColumnsList = new List<Columns>() };
            foreach (var container in descriptions)
            {
                item.ColumnsList.Add(new Columns() { Name = container.ColumnName, Description = container.Description });
            }
            list.Add(item);
        }
        return list;
    }
}