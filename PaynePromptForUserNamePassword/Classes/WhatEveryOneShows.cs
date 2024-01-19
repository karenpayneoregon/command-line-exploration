using CommandLine;

namespace PaynePromptForUserNamePassword.Classes;

public class WhatEveryOneShows
{
    [Option('r', "read", Required = false, HelpText = "Input files to be processed.")]
    public IEnumerable<string> InputFiles { get; set; }

    // Omitting long name, defaults to name of property, ie "--verbose"
    [Option(Default = false, HelpText = "Prints all messages to standard output.")]
    public bool Verbose { get; set; }

    [Option("stdin", Default = false, HelpText = "Read from stdin")]
    public bool stdin { get; set; }

    [Value(0, MetaName = "offset", HelpText = "File offset.")]
    public long? Offset { get; set; }

    [Option('c', "concurrent", Required = false, HelpText = "Number of concurrent requests", Default = 1)]
    public int ConcurrentCount { get; set; }
}