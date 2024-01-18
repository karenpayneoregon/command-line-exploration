using System.Text;

namespace Shortcuts.Models;
public class Shortcut
{
    /// <summary>
    /// Description of a single shortcut
    /// </summary>
    public string Description { get; set; }
    /// <summary>
    /// Pad <see cref="Description"/> for table display
    /// </summary>
    /// <param name="length"></param>
    /// <returns></returns>
    public string Text(int length) => $"{FormatColors}{Description.PadRight(length)}[/]";

    /// <summary>
    /// One or more characters to make up the shortcut
    /// </summary>
    public List<ShortCutKey> Keys { get; set; } 
    /// <summary>
    /// Foreground and background colors for table
    /// </summary>
    public string FormatColors { get; set; }
    /// <summary>
    /// Provides the shortcut keys ready for display
    /// </summary>
    /// <returns></returns>
    public string Combination()
    {
        StringBuilder builder = new();
        foreach (var key in Keys)
        {
            builder.Append($"{FormatColors}{key}[/] + ");
        }

        return builder.ToString()[..^3];
    }
}