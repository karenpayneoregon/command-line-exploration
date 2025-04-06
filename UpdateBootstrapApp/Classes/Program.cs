using System.Runtime.CompilerServices;

// ReSharper disable once CheckNamespace
namespace UpdateBootstrapApp;
internal partial class Program
{
    [ModuleInitializer]
    public static void Init()
    {
        Console.Title = "Update Bootstrap to version 5.3.4";
        WindowUtility.SetConsoleWindowPosition(WindowUtility.AnchorWindow.Center);
    }
}
