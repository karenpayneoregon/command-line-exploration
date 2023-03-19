# Working with command line (C#)

Learn how to create .NET tools which can assist with automating task which can be installed globally on a computer.

:fleur_de_lis: [Article](https://dev.to/karenpayneoregon/c-net-tools-withsystemcommandline-2nc2)

## Skill level

To write a simple .NET tool requires basic understand of the C# language and basics of working with classes. Consider starting with the project CommandArgsConsoleApp1.

For writing robust .NET tool a solid understanding of C# is required along with asynchronous programming and writing events.


## What is a global tool?

A .NET tool is a special NuGet package that contains a console application. You can install a tool on your machine in the [following ways](https://learn.microsoft.com/en-us/dotnet/core/tools/global-tools).

There are over 3,000 preexisting [packages](https://www.nuget.org/packages?packagetype=dotnettool) to choose from such as [dotnet-ef](https://www.nuget.org/packages/dotnet-ef/7.0.4) which generates code for a DbContext and entity types for a database. In order for this command to generate an entity type, the database table must have a primary key.

To create a .NET tool there are two NuGet packages

- Microsoft.Extensions.Configuration.CommandLine [package](https://www.nuget.org/packages/Microsoft.Extensions.Configuration.CommandLine/7.0.0?_src=template)
- CommandLineParser [package](https://www.nuget.org/packages/CommandLineParser/2.8.0?_src=template)

In this article, Microsoft.Extensions.Configuration.CommandLine will be used which is currently considered in preview while CommandLineParser is considered a mature package.

## Advise before creating a tool

Rather than try and figure out what to create a tool for, instead consider task done that require several steps that can be done in C#. Next, create a console project without Microsoft.Extensions.Configuration.CommandLine package, get it working perfectly. Next, create a new console project and bring in the code from the first project, now with the Microsoft.Extensions.Configuration.CommandLine package. The alternate is to create one project and simple write the code if you feel comfortable with the basics presented below.

## Creating a simple tool

Let's start off simple, in this sample, get first and last name.


Create a new Console project, once created double click on the project name in Solution Explorer. Remove current contents in place of the following.

> **Note**
> In the section below for installing and uninstalling YourProjectName needs to be replaced with the name of this project. In the supplied example in the GitHub repository the project name is KP_CommandLineBase.

```xml
<Project Sdk="Microsoft.NET.Sdk">

    <PropertyGroup>
        <OutputType>Exe</OutputType>
        <TargetFramework>net7.0</TargetFramework>
        <ImplicitUsings>enable</ImplicitUsings>
        <Nullable>enable</Nullable>
        <PackAsTool>true</PackAsTool>
        <ToolCommandName>hello</ToolCommandName>
        <PackageOutputPath>./nupkg</PackageOutputPath>
        <GeneratePackageOnBuild>True</GeneratePackageOnBuild>
        <Version>2.0.0</Version>
    </PropertyGroup>

    <ItemGroup>
        <PackageReference Include="System.CommandLine" Version="2.0.0-beta4.22272.1" />
    </ItemGroup>
</Project>
```

Save the file. 

Next, create a folder named `Classes`, add a class named `MainOperation` which will be called in Program.Main below.

Add the following code to MainOperations class

```csharp
internal class MainOperations
{
    public static void MainCommand(string firstName, string lastName)
    {
        Console.WriteLine($"Hello {firstName} {lastName}");
    }
}
```


Next, open Program.cs and replace current Program code with the following.

```csharp
internal class Program
{
    static async Task Main(string[] args)
    {
        var firstNameOption = new Option<string>("--first")
        {
            Description = "First name",
            IsRequired = true
        };
        firstNameOption.AddAlias("-f");

        var lastNameOption = new Option<string>("--last")
        {
            Description = "last name",
            IsRequired = true
        };
        lastNameOption.AddAlias("-l");

        RootCommand rootCommand = new("Example for a basic command line tool")
        {
            firstNameOption,
            lastNameOption
        };

        rootCommand.SetHandler(MainOperations.MainCommand, firstNameOption, lastNameOption);

        var commandLineBuilder = new CommandLineBuilder(rootCommand);

        commandLineBuilder.AddMiddleware(async (context, next) =>
        {
            await next(context);
        });

        commandLineBuilder.UseDefaults();
        Parser parser = commandLineBuilder.Build();

        await parser.InvokeAsync(args);

    }
}
```

- firstNameOption and lastNameOpen of type [Option](https://learn.microsoft.com/en-us/dotnet/standard/commandline/syntax#options) define expected parameters.
    - For first name `--first` or `-f` e.g. -f Karen
    - For last name `--last` or `-l` e.g. -f Payne
 - [RootCommand](https://learn.microsoft.com/en-us/dotnet/standard/commandline/syntax#root-commands) rootCommand represents the main action that the application performs
- rootCommand.SetHandler defines, the method to handle actions passed which in this case are firstNameOption and lastNameOption
- var commandLineBuilder = new CommandLineBuilder(rootCommand); enables composition of command line configurations.
-  commandLineBuilder.AddMiddleware(async (context, next) ... see [How to use middleware in System.CommandLine](https://learn.microsoft.com/en-us/dotnet/standard/commandline/use-middleware)
- commandLineBuilder.[UseDefaults](https://learn.microsoft.com/en-us/dotnet/api/system.commandline.builder.commandlinebuilderextensions.usedefaults?view=system-commandline#system-commandline-builder-commandlinebuilderextensions-usedefaults(system-commandline-builder-commandlinebuilder))();
- Parser parser = commandLineBuilder.[Build](https://learn.microsoft.com/en-us/dotnet/api/system.commandline.builder.commandlinebuilder.build?view=system-commandline#system-commandline-builder-commandlinebuilder-build)();
- await parser.[InvokeAsync](https://learn.microsoft.com/en-us/dotnet/api/system.commandline.commandextensions.invokeasync#system-commandline-commandextensions-invokeasync(system-commandline-command-system-string-system-commandline-iconsole))(args); parses and invokes the given command

###  Install/uninstall

- Build the project
- Open a command prompt or PowerShell to the root of this project
- Enter the following to install 
    - dotnet tool install --global --add-source ./nupkg `YourProjectName`
- Enter the following to uninstall
    - dotnet tool uninstall  -g `YourProjectName` to uninstall the tool.

After installation

```
You can invoke the tool using the following command: hello
Tool 'YourProjectName' (version '2.0.0') was successfully installed.
```

Run the tool (hello is defined in the .csproj file `ToolCommandName`)

```
hello -f Karen -l Payne
```

After uninstall

```
Tool 'YourProjectName' (version '2.0.0') was successfully uninstalled.
```

## Samples as tools

There are two projects that are practical examples for .NET tools. 

The first is Holidays which gets all holidays for the current year by two character country code. Once installed, pass `-c XX` where 'XX' is a [country code](https://github.com/nager/Nager.Date/blob/main/src/Nager.Date/CountryCode.cs). This makes a call to https://date.nager.at/api/v3/publicholidays/ to get holidays for the current year and country.


The second DirectoryCount expects `-d` followed by a folder name which when executed returns total folder and file count of the folder passed. If the user does not have permissons the exception is caught and presented, if the folder does not exists, that is captured also.

# Included projects

Are listed below, the project CommandArgsConsoleAppHelp showcases how to override the [default help](https://learn.microsoft.com/en-us/dotnet/standard/commandline/customize-help) which can better assist users in the case where the default help does not fully convey usage e.g. a parameter is –name which expects first and last name. They may not know to wrap as follows –name “Karen Payne” while a better approach would be –first Karen –last Payne.

| Project        |   Description    |
|:------------- |:-------------|
| KP_CommandLineBase | A very basic tool example to accept first and last name, both required to display to the console. |
| CommandArgsConsoleApp1 | This project performs the same operations as in the project CommandArgsConsoleApp2 but not taking full advantage of the package System.CommandLine. |
| CommandArgsConsoleApp2 | A base template for working with System.CommandLine which uses SeriLog sink to write to a SQL-Server database. |
| CommandArgsConsoleAppHelp | Demonstrates how to override the help section for working with  System.CommandLine. |
| CommandArgsConsoleSubCommands |  Shows how to use verbs and commands to read a file in chunks and display to the screen. This project is based off a Microsoft code sample and enhanced.|
| DirectoryCount| This project demonstrates how to get a folder and file count recursively for a folder name passed. If the user lacks proper permissions an exception is caught and thrown. |
| Holidays | This project shows holidays by two letter country code for the current year and is setup to run as a dot net tool with instructions in its read me file. |

## See also

- System.CommandLine [overview](https://learn.microsoft.com/en-us/dotnet/standard/commandline/)
- [command-line-api](https://github.com/dotnet/command-line-api)
- [How to manage .NET tools](https://learn.microsoft.com/en-us/dotnet/core/tools/global-tools)
- [Mark of the web remover](https://dev.to/karenpayneoregon/mark-of-the-web-remover-14kc) A utility to remove mark of the web from a folder recursively to all sub-folders.

## Source code

Clone the following [GitHub repository](https://github.com/karenpayneoregon/command-line-exploration) which was created with Microsoft VS2022 using C#11.