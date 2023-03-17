using Microsoft.Extensions.Configuration;
using Serilog;
using Serilog.Sinks.MSSqlServer;

namespace CommandArgsConsoleApp2.Classes;
internal class SetupLogging
{
    /// <summary>
    /// Initialize SeriLog for working with SQL-Server
    /// </summary>
    /// <param name="environment">Application runtime Environment</param>
    public static void Initialize(Environment environment)
    {
        const string connectionStringName = "LogDatabase";
        const string schemaName = "dbo";
        const string tableName = "LogEvents";

        IConfigurationRoot configuration = new ConfigurationBuilder()
            .AddJsonFile($"appsettings.{environment}.json", optional: true, reloadOnChange: true)
            .Build();

        var columnOptionsSection = configuration.GetSection("Serilog:ColumnOptions");
        var sinkOptionsSection = configuration.GetSection("Serilog:SinkOptions");

        Log.Logger = new LoggerConfiguration()
            .WriteTo.MSSqlServer(
                connectionString: connectionStringName,
                sinkOptions: new MSSqlServerSinkOptions
                {
                    TableName = tableName,
                    SchemaName = schemaName,
                    AutoCreateSqlTable = true
                },
                sinkOptionsSection: sinkOptionsSection,
                appConfiguration: configuration,
                columnOptionsSection: columnOptionsSection)
            .CreateLogger();
    }
}
