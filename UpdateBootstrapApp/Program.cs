using System.Diagnostics;
using System.Text;
using UpdateBootstrapApp.Models;

namespace UpdateBootstrapApp;

internal partial class Program
{
    static async Task Main(string[] args)
    {
        var test = LibManCommandSettings.LoadFromConfiguration();

        Console.WriteLine(Path.Combine(Directory.GetCurrentDirectory()));
        // do not touch libman.json as I'm only targeting new projects
        if (File.Exists(Path.Combine(Directory.GetCurrentDirectory(), "libman.json")))
        {
            AnsiConsole.MarkupLine("[white]libman.json[/] [cyan]exists[/], [white]no action taken[/]");
        }
        else
        {
            //await WorkOnNewProject();
            // TODO write config file to select version in appsettings.json
            await UpdateToVersion5_3_4();
            AnsiConsole.MarkupLine("[yellow]Bootstrap is now[/] [white]5.3.4[/]");
        }
        
        Console.ReadLine();
    }

    /// <summary>
    /// Updates the current project to use Bootstrap version 5.3.1 by removing any existing Bootstrap files,
    /// creating a batch file for LibMan commands, and executing it to install the specified Bootstrap version.
    /// </summary>
    /// <remarks>
    /// This method performs the following steps:
    /// 1. Deletes the existing Bootstrap directory if it exists.
    /// 2. Generates a batch file containing LibMan commands to initialize and install Bootstrap.
    /// 3. Executes the batch file to install the specified Bootstrap files.
    /// 4. Deletes the batch file after execution.
    /// </remarks>
    /// <returns>A <see cref="Task"/> that represents the asynchronous operation.</returns>
    private static async Task WorkOnNewProject()
    {
        // remove current bootstrap
        if (Directory.Exists("wwwroot\\lib\\bootstrap"))
        {
            Directory.Delete("wwwroot\\lib\\bootstrap", true);
        }
        
        // create batch file for libman
        StringBuilder builder = new();
        builder.AppendLine("libman init --default-provider cdnjs");
        builder.AppendLine("libman install bootstrap@5.3.1 " + 
                           "--destination wwwroot/lib/bootstrap/dist " + 
                           "--files css/bootstrap.min.css " + 
                           "--files css/bootstrap.min.css.map " + 
                           "--files js/bootstrap.bundle.min.js " + 
                           "--files js/bootstrap.bundle.min.js.map");

        await File.WriteAllTextAsync("install.bat", builder.ToString());

        await Task.Delay(1000);

        var batchCommandFile = Path.Combine(Directory.GetCurrentDirectory(), "install.bat");

        ProcessStartInfo psi = new ProcessStartInfo
        {
            WorkingDirectory = Directory.GetCurrentDirectory(),
            FileName = batchCommandFile,
            RedirectStandardOutput = true,
        };

        Process process = Process.Start(psi);

        process!.WaitForExit(-1);
        await Task.Delay(1000);

        File.Delete(batchCommandFile);
    }
    /// <summary>
    /// Updates the current project to use Bootstrap version 5.3.4 by removing any existing Bootstrap files,
    /// creating a batch file for LibMan commands, and executing it to install the specified Bootstrap version.
    /// </summary>
    /// <remarks>
    /// This method performs the following steps:
    /// 1. Deletes the existing Bootstrap directory if it exists.
    /// 2. Generates a batch file containing LibMan commands to initialize and install Bootstrap.
    /// 3. Executes the batch file and captures its output and errors.
    /// 4. Deletes the batch file after execution.
    /// </remarks>
    /// <returns>A <see cref="Task"/> that represents the asynchronous operation.</returns>
    private static async Task UpdateToVersion5_3_4()
    {

        // Remove current bootstrap
        if (Directory.Exists("wwwroot\\lib\\bootstrap"))
        {
            Directory.Delete("wwwroot\\lib\\bootstrap", true);
        }

        // Create batch file for libman
        StringBuilder builder = new();
        builder.AppendLine("libman init --default-provider jsdelivr");
        builder.AppendLine("libman install bootstrap@5.3.4 --destination wwwroot/lib/bootstrap --files dist/css/* --files dist/js/*");

        await File.WriteAllTextAsync("install.bat", builder.ToString());

        await Task.Delay(1000); // Slight delay to ensure file write

        var batchCommandFile = Path.Combine(Directory.GetCurrentDirectory(), "install.bat");

        ProcessStartInfo psi = new()
        {
            FileName = batchCommandFile,
            WorkingDirectory = Directory.GetCurrentDirectory(),
            RedirectStandardOutput = true,
            RedirectStandardError = true,
            UseShellExecute = false,
            CreateNoWindow = true,
        };

        using Process process = new() { StartInfo = psi };

        StringBuilder outputBuilder = new();
        StringBuilder errorBuilder = new();

        process.OutputDataReceived += (sender, e) => { if (e.Data != null) outputBuilder.AppendLine(e.Data); };
        process.ErrorDataReceived += (sender, e) => { if (e.Data != null) errorBuilder.AppendLine(e.Data); };

        process.Start();
        process.BeginOutputReadLine();
        process.BeginErrorReadLine();

        await process.WaitForExitAsync();

        if (process.ExitCode != 0)
        {
            Console.WriteLine("Error during batch execution:");
            Console.WriteLine(errorBuilder.ToString());
        }
        else
        {
            Console.WriteLine("Batch execution output:");
            Console.WriteLine(outputBuilder.ToString());
        }

        File.Delete(batchCommandFile);
    }

}