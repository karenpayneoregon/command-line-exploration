using Microsoft.Extensions.Configuration;

namespace CommandArgsConsoleApp2.Classes;

public class Configurations
{
    public static IConfigurationRoot GetConfigurationRoot(string environment) =>
        new ConfigurationBuilder()
            .SetBasePath(AppDomain.CurrentDomain.BaseDirectory)
            .AddJsonFile($"appsettings.{environment}.json",
                optional: true,
                reloadOnChange: true)
            .Build();
}