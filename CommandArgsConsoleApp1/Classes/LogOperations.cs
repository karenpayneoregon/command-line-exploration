using System.Text.Json;
using CombinedConfigDemo.Models;
using CommandArgsConsoleApp1.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Serilog;
using Spectre.Console;

namespace CommandArgsConsoleApp1.Classes;

internal class LogOperations
{

    public static void Logging(IConfigurationRoot config)
    {
        var logOption = config["log"];

        if (logOption is not null)
        {
            if (bool.TryParse(config["log"], out var useLogging))
            {
                if (useLogging)
                {
                    CreateSomeLogs();
                }
            }
        }
    }
    /// <summary>
    /// Create various types of log entries
    /// </summary>
    public static void CreateSomeLogs()
    {
        /*
         * Local method for truncating log table
         */
        static void TruncateLogTable()
        {
            using var context = new LogContext();

            var count = context.LogEvents.Count();

            if (count > 0)
            {
                AnsiConsole.MarkupLine("[cyan]Truncating table...[/]");
                context.Database.ExecuteSqlRaw($"TRUNCATE TABLE [{nameof(LogEvents)}]");
            }
        }
        
        TruncateLogTable();

        Log.Information("Hello {Name} from thread {ThreadId}", 
            System.Environment.GetEnvironmentVariable("USERNAME"), Thread.CurrentThread.ManagedThreadId);

        Log.Warning("Bad lat long {@Position}", new { Lat = 25, Long = 134 });

        Log.Error(new JsonException("Something is wrong"), "Json issue");

        Log.Error(new FileNotFoundException("Customers.json"), "Customers.json");
        Log.Information("Bye");

        Log.CloseAndFlush();
        AnsiConsole.MarkupLine("[cyan]Finished logging to database[/]");
    }
}
