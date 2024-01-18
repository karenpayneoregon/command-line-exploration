using SqlServerIndices.Classes;

namespace SqlServerIndices;

internal partial class Program
{
    static void Main(string[] args)
    {
        Console.Title = "";
        Worker.Execute();
    }
}