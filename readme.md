# Working with command line (C#)

Learn how to create .NET tools which can assist with automating task which can be installed globally on a computer.

## What is a global tool?

A .NET tool is a special NuGet package that contains a console application. You can install a tool on your machine in the [following ways](https://learn.microsoft.com/en-us/dotnet/core/tools/global-tools).

There are over 3,000 preexisting [packages](https://www.nuget.org/packages?packagetype=dotnettool) to choose from such as [dotnet-ef](https://www.nuget.org/packages/dotnet-ef/7.0.4) which generates code for a DbContext and entity types for a database. In order for this command to generate an entity type, the database table must have a primary key.

To create a .NET tool there are two NuGet packages

- Microsoft.Extensions.Configuration.CommandLine [package](https://www.nuget.org/packages/Microsoft.Extensions.Configuration.CommandLine/7.0.0?_src=template)
- CommandLineParser [package](https://www.nuget.org/packages/CommandLineParser/2.8.0?_src=template)

In this article, Microsoft.Extensions.Configuration.CommandLine will be used which is currently considered in preview while CommandLineParser is considered a mature package.
 

| Project        |   Description    |
|:------------- |:-------------|
| CommandArgsConsoleApp1 | This project performs the same operations as in the project CommandArgsConsoleApp2 but not taking full advantage of the package System.CommandLine. |
| CommandArgsConsoleApp2 | A base template for working with System.CommandLine which uses SeriLog sink to write to a SQL-Server database. |
| CommandArgsConsoleAppHelp | Demonstrates how to override the help section for working with  System.CommandLine. |
| CommandArgsConsoleSubCommands |  Shows how to use verbs and commands to read a file in chunks and display to the screen. This project is based off a Microsoft code sample and enhanced.|
| DirectoryCount| This project demonstrates how to get a folder and file count recursively for a folder name passed. If the user lacks proper permissions an exception is caught and thrown. |
| Holidays | This project shows holidays by two letter country code for the current year and is setup to run as a dot net tool with instructions in its read me file. |
