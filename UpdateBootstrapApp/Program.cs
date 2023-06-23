using System.Diagnostics;
using System.Text;

namespace UpdateBootstrapApp;

internal partial class Program
{
    static async Task Main(string[] args)
    {
        // do not touch libman.json as I'm only targeting new projects
        if (File.Exists(Path.Combine(Directory.GetCurrentDirectory(), "libman.json")))
        {
            AnsiConsole.MarkupLine("[white]libman.json[/] [cyan]exists[/], [white]no action taken[/]");
        }
        else
        {
            await WorkOnNewProject();
            AnsiConsole.MarkupLine("[yellow]Bootstrap is now[/] [white]5.3.0[/]");
        }
        
        Console.ReadLine();
    }

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
        builder.AppendLine("libman install bootstrap@5.3.0 " + 
                           "--destination wwwroot/lib/bootstrap/dist " + 
                           "--files css/bootstrap.min.css --files css/bootstrap.min.css.map " + 
                           "--files js/bootstrap.bundle.min.js --files js/bootstrap.bundle.min.js.map");

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

        // clean up
        File.Delete(batchCommandFile);
    }
}