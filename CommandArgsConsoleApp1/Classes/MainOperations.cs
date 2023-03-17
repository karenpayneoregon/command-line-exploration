using Microsoft.Extensions.Configuration;
using Spectre.Console;

namespace CommandArgsConsoleApp1.Classes;
internal class MainOperations
{
    /// <summary>
    /// Check if user name has been passed via command line argument and
    /// if so display the name.
    /// </summary>
    /// <param name="config"></param>
    public static void UserNameCheck(IConfigurationRoot config)
    {
        AnsiConsole.MarkupLine("[cyan]Checking user name from command line[/]");
        var user = config["username"];

        if (config["username"] is not null)
        {
            Console.WriteLine($"Hello {user}");
        }
        else
        {
            Console.WriteLine("User name not provided");
        }
    }

    /// <summary>
    /// Demo for reading from an environment appsettings file  
    /// </summary>
    /// <param name="config">IConfigurationRoot</param>
    public static void ViewEnvironment(IConfigurationRoot config)
    {
        AnsiConsole.MarkupLine("[cyan]Viewing environment items from appsettings[/]");

        if (config["Environment"] is not null)
        {
            if (Enum.TryParse(config["Environment"], true, out Environment environment))
            {
                Console.WriteLine($"Use {environment} environment");

                List<KeyValuePair<string, string>> settings = Configurations.GetConfigurationRoot1(environment.ToString());

                // uncomment to see all keys/values
                //Console.WriteLine(new string('-', 50));
                //foreach (var (key, value) in settings)
                //{
                //    Console.WriteLine($"{key}  {value}");
                //}
                //Console.WriteLine(new string('-', 50));

                Console.WriteLine($"                         Log database = {settings.FirstOrDefault(x => 
                    x.Key == "ConnectionStrings:LogDatabase").Value}");

                Console.WriteLine($"      Serilog:SinkOptions:batchPeriod = {settings.FirstOrDefault(x => 
                    x.Key == "Serilog:SinkOptions:batchPeriod").Value}");

                Console.WriteLine($"Serilog:SinkOptions:batchPostingLimit = {settings.FirstOrDefault(x => 
                    x.Key == "Serilog:SinkOptions:batchPostingLimit").Value}");

            }
        }
        else
        {
            Console.WriteLine("argument not passed or not spelled right");
        }
    }
}
