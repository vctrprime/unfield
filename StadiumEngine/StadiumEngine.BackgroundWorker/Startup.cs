#nullable enable
using System.Security.Claims;
using System.Text.Json.Serialization;
using Hangfire;
using HangfireBasicAuthenticationFilter;
using Microsoft.AspNetCore.HttpOverrides;
using Microsoft.AspNetCore.Server.Kestrel.Core;
using Serilog;
using Serilog.Debugging;
using Serilog.Events;
using Serilog.Filters;
using StadiumEngine.BackgroundWorker.Infrastructure.Extensions;
using StadiumEngine.BackgroundWorker.Jobs.Dashboard;
using StadiumEngine.Common.Configuration;

namespace StadiumEngine.BackgroundWorker;

/// <summary>
///     Установочный класс
/// </summary>
public class Startup
{
    /// <summary>
    ///     Установочный класс
    /// </summary>
    public Startup( IConfiguration configuration )
    {
        Configuration = configuration;
    }

    /// <summary>
    ///     Объект конфигурации
    /// </summary>
    public IConfiguration Configuration { get; }

    /// <summary>
    ///     Конфигурация сервисов
    /// </summary>
    /// <param name="services"></param>
    public void ConfigureServices( IServiceCollection services )
    {
        services.AddSingleton<StorageConfig>();
        services.AddSingleton<UtilsConfig>();
        services.AddSingleton<UtilServiceConfig>();
        services.AddSingleton<EnvConfig>();
        
        string? connectionString = Configuration.GetConnectionString( "MainDbConnection" );
        string? archiveConnectionString = Configuration.GetConnectionString( "ArchiveDbConnection" );

        SelfLog.Enable( msg => Console.WriteLine( $"Logging Process Error: {msg}" ) );
        Log.Logger = new LoggerConfiguration()
            .MinimumLevel.Information()
            .Filter.ByExcluding( Matching.FromSource( "Microsoft.AspNetCore.Diagnostics.ExceptionHandlerMiddleware" ) )
            .Filter.ByExcluding( Matching.FromSource( "Microsoft.EntityFrameworkCore.Database.Command" ) )
            .WriteTo.Console()
            .WriteTo.PostgreSQL(
                connectionString,
                "PUBLIC.BW_LOG_ERRORS",
                needAutoCreateTable: true,
                restrictedToMinimumLevel: LogEventLevel.Error )
            .CreateLogger();

        services.RegisterModules( connectionString, archiveConnectionString );

        services.AddControllersWithViews().AddJsonOptions(
                options => { options.JsonSerializerOptions.Converters.Add( new JsonStringEnumConverter() ); } )
            .AddNewtonsoftJson();

        services.AddHttpContextAccessor();
        services.AddTransient( s => s.GetService<IHttpContextAccessor>()?.HttpContext?.User ?? new ClaimsPrincipal() );

        services.Configure<KestrelServerOptions>( Configuration.GetSection( "Kestrel" ) );
    }

    /// <summary>
    ///     Конфигурация конвейера
    /// </summary>
    /// <param name="app"></param>
    /// <param name="env"></param>
    /// <param name="logger"></param>
    public void Configure( IApplicationBuilder app, IWebHostEnvironment env, ILogger<Startup> logger )
    {
        if ( env.IsDevelopment() )
        {
            app.UseDeveloperExceptionPage();
        }
        else
        {
            app.UseForwardedHeaders(
                new ForwardedHeadersOptions
                {
                    ForwardedHeaders = ForwardedHeaders.XForwardedFor | ForwardedHeaders.XForwardedProto
                } );
            app.UseExceptionHandler( "/Error" );
            // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
            app.UseHsts();
            app.UseHttpsRedirection();
        }

        app.UseRouting();
        app.UseHangfireDashboard(
            "/hangfire",
            new DashboardOptions()
            {
                DashboardTitle = "Hangfire Dashboard",
                Authorization = new[]
                {
                    new HangfireCustomBasicAuthenticationFilter
                    {
                        User = Configuration.GetSection( "HangfireCredentials:UserName" ).Value,
                        Pass = Configuration.GetSection( "HangfireCredentials:Password" ).Value
                    }
                }
            } );
        
        #region Jobs

        RecurringJob.AddOrUpdate<IDashboardCalculatorJob>(
            "Calculate Dashboards Data",
            x => x.Calculate(),
            "0 0 * * * * "
        );

        #endregion
        
        app.UseEndpoints(
            endpoints =>
            {
                endpoints.MapControllerRoute(
                    "default",
                    "{controller}/{action=Index}/{id?}" );
                endpoints.MapHangfireDashboard();
            } );
    }
}