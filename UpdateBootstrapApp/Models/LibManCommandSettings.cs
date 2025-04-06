
using Microsoft.Extensions.Configuration;

namespace UpdateBootstrapApp.Models;


/// <summary>
/// Represents the settings for executing LibMan commands, including loading configurations
/// from a specified JSON file.
/// </summary>
/// <remarks>
/// This class provides functionality to load and bind LibMan command settings from a configuration
/// file, enabling the execution of predefined commands for managing client-side libraries.
///
/// CURRENTLY NOT USED
/// 
/// </remarks>
public class LibManCommandSettings
{
    public List<string> LibManCommands { get; set; }

    public static LibManCommandSettings LoadFromConfiguration(string configPath = "appsettings.json")
    {
        if (!File.Exists(configPath))
            throw new FileNotFoundException($"Configuration file '{configPath}' not found.");

        var configuration = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile(configPath, optional: false, reloadOnChange: true)
            .Build();

        var settings = new LibManCommandSettings();
        configuration.Bind(settings);

        return settings;
    }

    /// <summary>
    /// Gets a single concatenated string representation of all LibMan commands, 
    /// with each command separated by a newline character.
    /// </summary>
    /// <value>
    /// A string containing all commands from the <see cref="LibManCommands"/> property, 
    /// joined by newline characters.
    /// </value>
    /// <remarks>
    /// This property provides a convenient way to retrieve all commands as a single 
    /// formatted string, suitable for display or logging purposes.
    /// </remarks>
    public string Command => string.Join(Environment.NewLine, LibManCommands);
}