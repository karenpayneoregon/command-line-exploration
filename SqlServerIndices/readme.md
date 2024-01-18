# About

Used to get all indexes for tables in a database. 

Database `server is set in `appsettings.json`

# Use as a tool

To install from the root folder of the project via a PowerShell command prompt

```
dotnet tool install --global --add-source ./nupkg SqlServerIndices
```

To uninstall from the root folder of the project via a PowerShell command prompt

```
dotnet tool uninstall -g SqlServerIndices
```

And to list all tools in the event you forget the actual command to start the tool or look in the project file.

```
dotnet tool list -g
```