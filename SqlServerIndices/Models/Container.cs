namespace SqlServerIndices.Models;
public class Container
{
    public string TableName { get; set; }
    public string IndexName { get; set; }
    public int IndexId { get; set; }
    public string ColumnName { get; set; }
    public override string ToString() => $"{TableName} - {ColumnName}";
}
