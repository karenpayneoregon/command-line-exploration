using System.Runtime.CompilerServices;
using Spectre.Console;

// ReSharper disable once CheckNamespace
namespace SqlServerIndices;
internal partial class Program
{
    [ModuleInitializer]
    public static void Init()
    {
        AnsiConsole.MarkupLine("");
        Console.Title = "Code sample";
        WindowUtility.SetConsoleWindowPosition(WindowUtility.AnchorWindow.Center);
    }
}
