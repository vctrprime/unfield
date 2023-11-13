#nullable enable
using System.Security.Claims;
using System.Text.Json.Serialization;
using Microsoft.AspNetCore.HttpOverrides;
using Microsoft.AspNetCore.Server.Kestrel.Core;
using Microsoft.AspNetCore.SpaServices.ReactDevelopmentServer;
using Microsoft.Extensions.FileProviders;
using Serilog;
using Serilog.Debugging;
using Serilog.Events;
using Serilog.Filters;
using StadiumEngine.BookingForm.Infrastructure.Extensions;
using StadiumEngine.BookingForm.Infrastructure.Middleware;
using StadiumEngine.Common.Configuration;

namespace StadiumEngine.BookingForm;

/// <summary>
///     Установочный класс
/// </summary>
public class Startup
{
    private readonly IWebHostEnvironment _environment;

    /// <summary>
    ///     Установочный класс
    /// </summary>
    public Startup( IConfiguration configuration,
        IWebHostEnvironment environment )
    {
        Configuration = configuration;
        _environment = environment;
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
        services.AddSingleton( Configuration.GetSection( "StorageConfig" ).Get<StorageConfig>() );
        
        string? connectionString = Configuration.GetConnectionString( "MainDbConnection" );
        
        SelfLog.Enable( msg => Console.WriteLine( $"Logging Process Error: {msg}" ) );
        Log.Logger = new LoggerConfiguration()
            .MinimumLevel.Information()
            .Filter.ByExcluding( Matching.FromSource( "Microsoft.AspNetCore.Diagnostics.ExceptionHandlerMiddleware" ) )
            .Filter.ByExcluding( Matching.FromSource( "Microsoft.EntityFrameworkCore.Database.Command" ) )
            .WriteTo.Console()
            .WriteTo.PostgreSQL(
                connectionString,
                "PUBLIC.LOG_ERRORS",
                needAutoCreateTable: true,
                restrictedToMinimumLevel: LogEventLevel.Error )
            .CreateLogger();
        
        services.RegisterModules( connectionString );
        
        services.AddControllersWithViews().AddJsonOptions(
                options => { options.JsonSerializerOptions.Converters.Add( new JsonStringEnumConverter() ); } )
            .AddNewtonsoftJson();

        services.AddHttpContextAccessor();
        services.AddTransient( s => s.GetService<IHttpContextAccessor>()?.HttpContext?.User ?? new ClaimsPrincipal() );

        services.Configure<KestrelServerOptions>( Configuration.GetSection( "Kestrel" ) );

        // In production, the React files will be served from this directory
        services.AddSpaStaticFiles( configuration => { configuration.RootPath = "ClientApp/build"; } );
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

        //ставим небольшую задержку на обработку запросов, для тестов скелетона и спиннера
        app.Use(
            async ( context, next ) =>
            {
                if ( context.Request.Path.Value != null && context.Request.Path.Value.Contains( "api/" ) )
                {
                    //Thread.Sleep(3000);
                }

                await next.Invoke();
            } );

        app.ConfigureExceptionHandler( logger );

        app.UseStaticFiles();

        StorageConfig storageConfig = Configuration.GetSection( "StorageConfig" ).Get<StorageConfig>();
        app.UseStaticFiles(
            new StaticFileOptions
            {
                FileProvider = new PhysicalFileProvider( storageConfig.ImageStorage ),
                RequestPath = "/legal-images"
            } );

        app.UseSpaStaticFiles();
        app.UseRouting();
        
        app.UseEndpoints(
            endpoints =>
            {
                endpoints.MapControllerRoute(
                    "default",
                    "{controller}/{action=Index}/{id?}" );
            } );

        app.UseSpa(
            spa =>
            {
                spa.Options.SourcePath = "ClientApp";

                if ( env.IsDevelopment() )
                {
                    spa.UseReactDevelopmentServer( "start" );
                }
            } );
    }
}