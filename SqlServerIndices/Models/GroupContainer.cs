namespace SqlServerIndices.Models;

public record GroupContainer(string TableName, IOrderedEnumerable<Container> containerList)
{
    public override string ToString() => TableName;

}