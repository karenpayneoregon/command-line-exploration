namespace CommandArgsConsoleApp1.Classes;
internal class SwitchOptions
{
    /// <summary>
    /// Allowable command line parameters
    /// </summary>
    /// <remarks>
    /// Less elegant than how it's done in CommandArgsConsoleApp2 project
    /// </remarks>
    public static Dictionary<string, string> Mappings => new()
    {
        { "--environment", "environment" },
        { "-e"           , "environment" },
        { "--username"   , "username" },
        { "-u"           , "username" },
        { "--log"        , "log" },
        { "-l"           , "log" },
    };
}
