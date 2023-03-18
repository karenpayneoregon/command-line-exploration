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

        var configuration = Configurations.GetConfigurationRoot(environment.ToString());
        var columnOptionsSection = configuration.GetSection("Serilog:ColumnOptions");
        var sinkOptionsSection = configuration.GetSection("Serilog:SinkOptions");

        /*
         * Shows how to read values to validate we are, in this case reading from appsettings correctly
         */
        ColumnOptions columnOptions = configuration.GetSection("Serilog").Get<ColumnOptions>();
        var timeStamp = columnOptions.TimeStamp.ColumnName;


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