﻿using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Serilog;
using Serilog.Formatting.Compact;
using Microsoft.Extensions.Logging;

namespace DsDelivery.WebApi;

public class Program
{
    public static void Main(string[] args)
    {
        IConfigurationRoot configuration = GetConfiguration();

        ConfiguraLog(configuration);

        try
        {
            Log.Information("Iniciando o WebApi");
            CreateHostBuilder(args).Build().Run();
        }
        catch (Exception ex)
        {
            Log.Fatal(ex, "Erro catastr�fico.");
            throw;
        }
        finally
        {
            Log.CloseAndFlush();
        }
    }

    private static void ConfiguraLog(IConfigurationRoot configuration)
    {
        Log.Logger = new LoggerConfiguration()
            .ReadFrom.Configuration(configuration)
            .CreateLogger();
    }

    private static IConfigurationRoot GetConfiguration()
    {
        string ambiente = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");

        var configuration = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json")
            .AddJsonFile($"appsettings.{ambiente}.json", optional: true)
            .Build();
        return configuration;
    }

    public static IHostBuilder CreateHostBuilder(string[] args) =>
        Host.CreateDefaultBuilder(args)
            .UseSerilog()
            .ConfigureWebHostDefaults(webBuilder =>
            {
                webBuilder.UseStartup<Startup>();
            });
}
