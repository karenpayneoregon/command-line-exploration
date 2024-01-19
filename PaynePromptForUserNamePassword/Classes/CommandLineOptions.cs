using CommandLine;

namespace PaynePromptForUserNamePassword.Classes
{
    public sealed class CommandLineOptions
    {
        [Option('u', "username", Required = true, HelpText = "Your username")]
        public string Username { get; set; }

        [Option('p', "password", Required = true, HelpText = "Your password")]
        public string Password { get; set; }

        // Omitting long name, defaults to name of property, ie "--verbose"
        [Option(Default = false, HelpText = "Prints all messages to standard output.")]
        public bool Verbose { get; set; }

    }
}