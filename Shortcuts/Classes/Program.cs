using System.Runtime.CompilerServices;

// ReSharper disable once CheckNamespace
namespace Shortcuts;
internal partial class Program
{
    [ModuleInitializer]
    public static void Init()
    {
        WindowUtility.SetConsoleWindowPosition(WindowUtility.AnchorWindow.Center);
    }
}
