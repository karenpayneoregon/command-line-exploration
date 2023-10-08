using System.Text.Json;
using Shortcuts.Models;

namespace Shortcuts.Classes;

public class FileOperations
{

    public static string FileName => @"C:\OED\Shortcuts\vs2022rs.json";

    public static List<Shortcut> ReadShortcuts()
    {
        return JsonSerializer.Deserialize<List<Shortcut>>(File.ReadAllText(FileName));
    }

    public static (List<Shortcut> shortcuts, int longText) Read()
    {
        var shortcuts = JsonSerializer.Deserialize<List<Shortcut>>(File.ReadAllText(FileName));
        var longText = shortcuts.Select(x => x.Description.Length).Max();

        return (shortcuts, longText + 1);
    }
}