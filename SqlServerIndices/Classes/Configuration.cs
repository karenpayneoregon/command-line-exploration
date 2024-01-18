using ConfigurationLibrary.Classes;
using Microsoft.Extensions.Configuration;
using SqlServerIndices.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace SqlServerIndices.Classes;
internal class Configuration
{
    [DebuggerStepThrough]
    public static ServerSettings ServerSettings()
    {
        var configuration = Builder();
        return configuration.GetSection(nameof(ServerSettings)).Get<ServerSettings>();
    }

    [DebuggerStepThrough]
    private static IConfigurationRoot Builder()
    {
        var builder = new ConfigurationBuilder().AddJsonFile("appsettings.json");
        var configuration = builder.Build();
        return configuration;
    }

}
