using System.Text.Json;

namespace SqlServerColumnDescriptions.Classes;

public class ServerConfiguration
{
    public string ServerName { get; set; }
}

public static class ConfigurationPaths
{
    public static string FileName => "appsettings.json";
    public static bool Exists => File.Exists(FileName);
}
public class ConfigurationMockup
{
    public static void Create()
    {
        ServerConfiguration configuration = new() { ServerName = "(localdb)\\MSSQLLocalDB" };
        string json = JsonSerializer.Serialize(configuration, new JsonSerializerOptions { WriteIndented = true });
        File.WriteAllText(ConfigurationPaths.FileName, json);
    }
}

public class AppConfiguration
{
    public static string GetServer()
    {
        if (ConfigurationPaths.Exists)
        {
            ServerConfiguration configuration = JsonSerializer.Deserialize<ServerConfiguration>(File.ReadAllText(ConfigurationPaths.FileName));

            return configuration!.ServerName;
        }
        else
        {
            throw new FileNotFoundException();
        }
    }
}