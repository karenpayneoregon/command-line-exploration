namespace SqlServerColumnDescriptions.Models;
public class DescriptionContainer
{
    public string ColumnName { get; set; }
    public int ColumnId { get; set; }
    public string Description { get; set; }
    public override string ToString() => $"{ColumnName} -> {Description}";

}
