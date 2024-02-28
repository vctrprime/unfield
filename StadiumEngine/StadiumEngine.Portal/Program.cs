using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using Serilog;

namespace StadiumEngine.Portal;

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
        AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);
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
        Host.CreateDefaultBuilder( args ).UseSerilog()
            .ConfigureWebHostDefaults(
                webBuilder =>
                {
                    webBuilder
                        .UseStartup<Startup>()
                        .ConfigureAppConfiguration(
                            ( builderContext, config ) =>
                            {
                                config
                                    .AddJsonFile( "appsettings.json", optional: true )
                                    .AddJsonFile( $"appsettings.{Environment.GetEnvironmentVariable( "ASPNETCORE_ENVIRONMENT" )}.json", optional: true );
                            } );
                } );
}