namespace Shortcuts.Models;

/// <summary>
/// Represents part of a shortcut combination.
/// </summary>
public class ShortCutKey
{
    /// <summary>
    /// Character for using in multi character shortcut
    /// </summary>
    public string Value { get; set; }
    public override string ToString() => Value;
}