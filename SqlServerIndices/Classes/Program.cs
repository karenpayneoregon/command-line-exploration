using System.Runtime.CompilerServices;
using Spectre.Console;

// ReSharper disable once CheckNamespace
namespace SqlServerIndices;
internal partial class Program
{
    /// <summary>
    /// For development only
    /// </summary>
    [ModuleInitializer]
    public static void Init()
    {
        AnsiConsole.MarkupLine("");
        Console.Title = "Code sample";
        WindowUtility.SetConsoleWindowPosition(WindowUtility.AnchorWindow.Center);
    }
}
