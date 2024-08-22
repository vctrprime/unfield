using System;
using System.IO;
using System.Reflection;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using Serilog;
using Serilog.Debugging;
using Serilog.Events;
using Serilog.Filters;

namespace StadiumEngine.Extranet;

/// <summary>
///     Точка входа
/// </summary>
public class Program
{
    /// <summary>
    ///     Точка входа
    /// </summary>
    public static void Main( string[] args )
    {
        AppContext.SetSwitch( "Npgsql.EnableLegacyTimestampBehavior", true );
        JsonConvert.DefaultSettings = () => new JsonSerializerSettings
        {
            ContractResolver = new CamelCasePropertyNamesContractResolver()
        };

        CreateHostBuilder( args ).Build().Run();
    }

    /// <summary>
    ///     Билд приложения
    /// </summary>
    /// <param name="args"></param>
    /// <returns></returns>
    public static IHostBuilder CreateHostBuilder( string[] args ) =>
        Host.CreateDefaultBuilder( args )
            .UseSerilog()
            .ConfigureWebHostDefaults(
                webBuilder =>
                {
                    webBuilder
                        .UseStartup<Startup>()
                        .ConfigureAppConfiguration(
                            ( builderContext, config ) =>
                            {
                                config
                                    .SetBasePath( Path.GetDirectoryName( Assembly.GetExecutingAssembly().Location ) )
                                    .AddJsonFile( "appsettings.json", optional: false )
                                    .AddJsonFile( "launch.settings.json", optional: true )
                                    .AddJsonFile(
                                        $"appsettings.{Environment.GetEnvironmentVariable( "ASPNETCORE_ENVIRONMENT" )}.json",
                                        optional: true )
                                    .AddUserSecrets<Startup>();
                            } );
                } );
}