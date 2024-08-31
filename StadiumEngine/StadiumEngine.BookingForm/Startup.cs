#nullable enable
using System.Globalization;
using System.Reflection;
using System.Security.Claims;
using System.Text.Json.Serialization;
using Microsoft.AspNetCore.DataProtection;
using Microsoft.AspNetCore.HttpOverrides;
using Microsoft.AspNetCore.Server.Kestrel.Core;
using Microsoft.AspNetCore.SpaServices.ReactDevelopmentServer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.FileProviders;
using Microsoft.OpenApi.Models;
using Newtonsoft.Json;
using StadiumEngine.BookingForm.Infrastructure;
using StadiumEngine.BookingForm.Infrastructure.Extensions;
using StadiumEngine.BookingForm.Infrastructure.Middleware;
using StadiumEngine.Common.Configuration.Infrastructure;
using StadiumEngine.Common.Configuration.Infrastructure.Extensions;
using StadiumEngine.Common.Configuration.Sections;

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
        LoadConfigurationResult loadConfigurationResult = services.LoadConfigurations( Configuration );
        Configurator.ConfigureLogger( loadConfigurationResult, "bf_log_errors" );
        
        services.AddDbContext<KeysContext>( options => options.UseNpgsql( loadConfigurationResult.ConnectionsConfig.MainDb ) );
        services.AddDataProtection()
            .PersistKeysToDbContext<KeysContext>()
            .SetApplicationName( "CustomerApp" );
        
        services.RegisterModules( loadConfigurationResult );
        
        services.AddControllersWithViews().AddJsonOptions(
                options => { options.JsonSerializerOptions.Converters.Add( new JsonStringEnumConverter() ); } )
            .AddNewtonsoftJson();

        services.AddHttpContextAccessor();
        services.AddTransient( s => s.GetService<IHttpContextAccessor>()?.HttpContext?.User ?? new ClaimsPrincipal() );

        services.Configure<KestrelServerOptions>( Configuration.GetSection( "Kestrel" ) );
        
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
                    options.ExpireTimeSpan = TimeSpan.FromDays( 7 );
                    options.SlidingExpiration = true;
                    options.Cookie.Name = "SE_CUSTOMER_TICKET";
                } );
        
        services.AddHttpContextAccessor();
        services.AddTransient( s => s.GetService<IHttpContextAccessor>()?.HttpContext?.User ?? new ClaimsPrincipal() );

        // In production, the React files will be served from this directory
        services.AddSpaStaticFiles( configuration => { configuration.RootPath = "ClientApp/build"; } );
        
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
                            Title = "Stadium Engine Booking Form API",
                            Version = "v1"
                        } );
                    string xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                    string xmlPath = Path.Combine( AppContext.BaseDirectory, xmlFile );
                    c.IncludeXmlComments( xmlPath, true );
                    string dtpXmlPath = Path.Combine( AppContext.BaseDirectory, "StadiumEngine.DTO.xml" );
                    c.IncludeXmlComments( dtpXmlPath );
                } );
        }
    }

    /// <summary>
    ///     Конфигурация конвейера
    /// </summary>
    /// <param name="app"></param>
    /// <param name="env"></param>
    /// <param name="logger"></param>
    public void Configure( IApplicationBuilder app, IWebHostEnvironment env, ILogger<Startup> logger )
    {
        CultureInfo.DefaultThreadCurrentCulture = CultureInfo.GetCultureInfo( "ru-RU" );
        CultureInfo.DefaultThreadCurrentUICulture = CultureInfo.GetCultureInfo( "ru-RU" );
        
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

        StorageConfig storageConfig = Configuration.GetSection( "StorageConfig" ).Get<StorageConfig>();
        app.UseStaticFiles(
            new StaticFileOptions
            {
                FileProvider = new PhysicalFileProvider( storageConfig.ImageStorage ),
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
            app.UseSwaggerUI( c => { c.SwaggerEndpoint( "/swagger/v1/swagger.json", "Stadium Engine Booking Form API" ); } );
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