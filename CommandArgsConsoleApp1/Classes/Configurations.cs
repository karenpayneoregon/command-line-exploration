using Microsoft.Extensions.Configuration;

namespace CommandArgsConsoleApp1.Classes;

public class Configurations
{
    /// <summary>
    /// Read appsettings.json by environment
    /// </summary>
    /// <param name="environment">to match <see cref="Environment"/></param>
    /// <returns></returns>
    public static IConfigurationRoot GetConfigurationRoot(string environment) =>
        new ConfigurationBuilder()
            .SetBasePath(AppDomain.CurrentDomain.BaseDirectory)
            .AddJsonFile($"appsettings.{environment}.json",
                optional: true,
                reloadOnChange: true)
            .Build();

    /// <summary>
    /// Strictly for iterating arguments
    /// </summary>
    /// <param name="environment">to match <see cref="Environment"/></param>
    /// <returns></returns>
    public static List<KeyValuePair<string, string>> GetConfigurationRoot1(string environment) =>
        new ConfigurationBuilder()
            .SetBasePath(AppDomain.CurrentDomain.BaseDirectory)
            .AddJsonFile($"appsettings.{environment}.json",
                optional: true,
                reloadOnChange: true)
            .Build().AsEnumerable().ToList();

}