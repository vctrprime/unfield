using System;
using System.IO;
using System.Reflection;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.DataProtection;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpOverrides;
using Microsoft.AspNetCore.Server.Kestrel.Core;
using Microsoft.AspNetCore.SpaServices.ReactDevelopmentServer;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.FileProviders;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using Newtonsoft.Json;
using StadiumEngine.WebUI.Infrastructure.Extensions;
using StadiumEngine.WebUI.Infrastructure.Middleware;

namespace StadiumEngine.WebUI;

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
        string folderPath =
            Path.Combine(
                Path.GetDirectoryName(
                    Assembly
                        .GetAssembly( typeof( Startup ) )?.Location ) ?? String.Empty,
                "keys" );

        services.AddDataProtection()
            .PersistKeysToFileSystem( new DirectoryInfo( folderPath ) )
            .SetApplicationName( $"StadiumEngine-{Environment.GetEnvironmentVariable( "ASPNETCORE_ENVIRONMENT" )}" )
            .SetDefaultKeyLifetime( TimeSpan.FromDays( 10000 ) )
            .DisableAutomaticKeyGeneration();

        services.RegisterModules();

        if ( _environment.IsDevelopment() )
        {
            services.AddSwaggerGen(
                c =>
                {
                    c.DescribeAllParametersInCamelCase();
                    c.SwaggerDoc(
                        "v1",
                        new OpenApiInfo
                        {
                            Title = "Stadium Engine API",
                            Version = "v1"
                        } );
                    string xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                    string xmlPath = Path.Combine( AppContext.BaseDirectory, xmlFile );
                    c.IncludeXmlComments( xmlPath, true );
                    string dtpXmlPath = Path.Combine( AppContext.BaseDirectory, "StadiumEngine.DTO.xml" );
                    c.IncludeXmlComments( dtpXmlPath );
                } );
        }

        services.AddAuthentication( "Identity.Core" )
            .AddCookie(
                "Identity.Core",
                options =>
                {
                    // Unauthorized return 401.
                    options.Events.OnRedirectToLogin = context =>
                    {
                        context.Response.StatusCode = 401;
                        context.Response.WriteAsync(
                            JsonConvert.SerializeObject(
                                new { Message = "Вы не авторизованы!" } ) );
                        return Task.CompletedTask;
                    };
                    // Access denied return 403.
                    /*options.Events.OnRedirectToAccessDenied = context =>
                    {
                        context.Response.StatusCode = 403;
                        return Task.CompletedTask;
                    };*/
                    options.ExpireTimeSpan = TimeSpan.FromHours( 8 );
                    options.SlidingExpiration = true;
                } );


        services.AddControllersWithViews().AddJsonOptions(
                options => { options.JsonSerializerOptions.Converters.Add( new JsonStringEnumConverter() ); } )
            .AddNewtonsoftJson();

        services.AddHttpContextAccessor();
        services.AddTransient( s => s.GetService<IHttpContextAccessor>().HttpContext.User );

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
        env.WriteReactEnvAppVersion();

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
        app.UseStaticFiles(
            new StaticFileOptions
            {
                FileProvider = new PhysicalFileProvider( Environment.GetEnvironmentVariable( "IMAGE_STORAGE" ) ),
                RequestPath = "/legal-images"
            } );

        app.UseSpaStaticFiles();
        app.UseRouting();
        app.UseAuthentication();
        app.UseAuthorization();

        if ( env.IsDevelopment() )
        {
            app.UseSwagger();

            // Enable middleware to serve swagger-ui (HTML, JS, CSS, etc.),
            // specifying the Swagger JSON endpoint.
            app.UseSwaggerUI( c => { c.SwaggerEndpoint( "/swagger/v1/swagger.json", "Stadium Engine API" ); } );
        }

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