namespace SqlServerIndices.Classes;

internal class SqlStatements
{
    public static string GetIndices => 
        """
        SELECT TableName = t.name,
               IndexName = ind.name,
               IndexId = ind.index_id,
               ColumnName = col.name 
        FROM sys.indexes ind
            INNER JOIN sys.index_columns ic
                ON ind.object_id = ic.object_id
                   AND ind.index_id = ic.index_id
            INNER JOIN sys.columns col
                ON ic.object_id = col.object_id
                   AND ic.column_id = col.column_id
            INNER JOIN sys.tables t
                ON ind.object_id = t.object_id
        WHERE ind.is_primary_key = 0
              AND ind.is_unique = 0
              AND ind.is_unique_constraint = 0
              AND t.is_ms_shipped = 0
        ORDER BY t.name,
                 ind.name,
                 ind.index_id,
                 ic.is_included_column,
                 ic.key_ordinal;
        """;

    public static string GetDatabaseNames 
        => "SELECT name FROM sys.databases WHERE name NOT IN ('master', 'tempdb', 'model', 'msdb') ORDER BY name";



}