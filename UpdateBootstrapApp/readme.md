# About

When creating a new ASP.NET Core/Razor Pages project in Microsoft Visual Studio 2022, Bootstrap 5.1.0 is installed.

At the current time, the present version of Bootstrap is 5.3.4 and to update there are two paths to upgrade Bootstrap.

> **Note**
> Not a dotnet tool and some of the instructions need to be updated as of April 2025.

## Options

### From Visual Studio

Right clicking in solution explorer in the project, select **Add**, **Client-Side library** then type **bootstrap** and you get bootstrap@5.3.4 (or if there is a newer version than it will show). Next click **Install**.

### From CLI 5.3 (not 5.3.4))

- In solution explorer, right click on wwwroot/lib/bootstrap
- Delete the folder

- Install [Libman CLI](https://learn.microsoft.com/en-us/aspnet/core/client-side/libman/libman-cli?view=aspnetcore-7.0#installation)

- Open a command window to the root of the project and enter

    - libman init --default-provider cdnjs
    - libman install bootstrap@5.3.0 --destination  wwwroot/lib/bootstrap/dist

In the root of the project libman.json is generated from the first command.

The second command installed version 5.3.0 of bootstrap.

```json
{
  "version": "1.0",
  "defaultProvider": "cdnjs",
  "libraries": [
    {
      "library": "bootstrap@5.3.0",
      "destination": "wwwroot/lib/bootstrap/dist"
    }
  ]
}
```

# Caveat

When installing from Visual Studio the libman interface allows selective installation of files while the CLI does not. In the current project it does selective installation of Bootstrap using `--files` argument.


## Current project

First checks if libman.json exist, if not contine, if exists do nothing.

Process

Remove the current Bootstrap folder

```csharp
Directory.Delete("wwwroot\\lib\\bootstrap", true);
```

Create a batch file to run libman

```csharp
StringBuilder builder = new();
builder.AppendLine("libman init --default-provider cdnjs");
builder.AppendLine("libman install bootstrap@5.3.0 " + 
                   "--destination wwwroot/lib/bootstrap/dist " + 
                   "--files css/bootstrap.min.css --files css/bootstrap.min.css.map " + 
                   "--files js/bootstrap.bundle.min.js --files js/bootstrap.bundle.min.js.map");

await File.WriteAllTextAsync("install.bat", builder.ToString());
```

Run the batch file

```csharp
var batchCommandFile = Path.Combine(Directory.GetCurrentDirectory(), "install.bat");

ProcessStartInfo psi = new ProcessStartInfo
{
    WorkingDirectory = Directory.GetCurrentDirectory(),
    FileName = batchCommandFile,
    RedirectStandardOutput = true,
};

Process process = Process.Start(psi);

process!.WaitForExit(-1);
```

Clean up

```csharp
File.Delete(batchCommandFile);
```

### How to use

Once compile, create an external command in Visual Studio.

In Visual Stuio

Open the `Tools` menu, click on `external tools...`

Click the Add button

Title: Update to Bootstrap 5.3.0
Command: traverse to the executable folder for this proect and select the executable

Arguments: $(ProjectDir)

Initial directory: $(ProjectDir)

Click OK.

To use the above, select a startup project in Solution explorer, select the new tool, done.

## Resources

- [Use LibMan with ASP.NET Core in Visual Studio](https://learn.microsoft.com/en-us/aspnet/core/client-side/libman/libman-vs?view=aspnetcore-7.0)
- [Use the LibMan CLI with ASP.NET Core](https://learn.microsoft.com/en-us/aspnet/core/client-side/libman/libman-cli?view=aspnetcore-7.0)
- cdnjs [Bootstrap](https://cdnjs.com/libraries/bootstrap)
- [Using LibMan To Manage Client-Side Dependencies](https://khalidabuhakmeh.com/using-libman-to-manage-client-side-dependencies)

## Closing

What has been provided can work for other client side libraries.


And for those hard core CLI developers, create a solution, project with Bootstrap 5.3.0

```
dotnet new razor --language "C#" --output FirstApp
rm .\FirstApp\wwwroot\lib\bootstrap -Recurse -Force
cd .\FirstApp\
libman init --default-provider cdnjs
libman install bootstrap@5.3.0 --destination wwwroot/lib/bootstrap/dist
libman install bootstrap@5.3.0 --destination wwwroot/lib/bootstrap/dist --files css/bootstrap.min.css --files css/bootstrap.min.css.map --files js/bootstrap.bundle.min.js --files js/bootstrap.bundle.min.js.map
```