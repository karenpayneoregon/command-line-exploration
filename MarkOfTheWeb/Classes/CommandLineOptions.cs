using CommandLine;
#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.

namespace MarkOfTheWeb.Classes;

public sealed class CommandLineOptions
{
    [Option('d', "dir", Required = true, HelpText = "Directory to remove mark of web")]
    public string Directory { get; set; }


    [Option(Default = false, HelpText = "Prints all messages to standard output.")]
    public bool Verbose { get; set; }


}