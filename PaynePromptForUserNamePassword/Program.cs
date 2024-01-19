using System.Diagnostics;
using CommandLine.Text;
using PaynePromptForUserNamePassword.Classes;

namespace PaynePromptForUserNamePassword
{
    class Program
    {
        static void Main(string[] args)
        {

            var title = new HeadingInfo(programName: "My super Get user id and password", version: "1.1.1");
            Debug.WriteLine(title);
            //CommandLineHelp.ParseArguments("--help".Split());
            CommandLineHelp.ParseArguments(args);

        }
    }
}
