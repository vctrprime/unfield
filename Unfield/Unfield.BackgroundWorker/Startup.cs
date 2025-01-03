#nullable enable
using System.Globalization;
using System.Security.Claims;
using System.Text.Json.Serialization;
using Hangfire;
using Hangfire.PostgreSql;
using HangfireBasicAuthenticationFilter;
using MassTransit;
using Microsoft.AspNetCore.HttpOverrides;
using Microsoft.AspNetCore.Server.Kestrel.Core;
using Serilog;
using Serilog.Debugging;
using Serilog.Events;
using Serilog.Filters;
using Unfield.BackgroundWorker.Infrastructure.Extensions;
using Unfield.Common.Configuration;
using Unfield.Common.Configuration.Infrastructure;
using Unfield.Common.Configuration.Infrastructure.Extensions;
using Unfield.Common.Configuration.Sections;
using Unfield.Handlers.Extensions;
using Unfield.Jobs.Recurring.Bookings;
using Unfield.Jobs.Recurring.Dashboard;
using Unfield.Jobs.Recurring.Notifications;
using ServiceCollectionExtensions = Unfield.Common.Configuration.Infrastructure.Extensions.ServiceCollectionExtensions;

namespace Unfield.BackgroundWorker;

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
        LoadConfigurationResult loadConfigurationResult = services.LoadConfigurations( Configuration );
        Configurator.ConfigureLogger( loadConfigurationResult, "bw_log_errors" );
        
        services.RegisterModules( loadConfigurationResult );
        
        services.AddControllersWithViews().AddJsonOptions(
                options => { options.JsonSerializerOptions.Converters.Add( new JsonStringEnumConverter() ); } )
            .AddNewtonsoftJson();

        services.AddHttpContextAccessor();
        services.AddTransient( s => s.GetService<IHttpContextAccessor>()?.HttpContext?.User ?? new ClaimsPrincipal() );

        services.Configure<KestrelServerOptions>( Configuration.GetSection( "Kestrel" ) );
        
        services.AddHangfire(
            configuration => configuration
                .UseSimpleAssemblyNameTypeSerializer()
                .UseRecommendedSerializerSettings()
                .UsePostgreSqlStorage( c => c.UseNpgsqlConnection( loadConfigurationResult.ConnectionsConfig.MainDb ) ) );
        services.AddHangfireServer( options =>
        {
            options.Queues = [ "default", "dashboards", "notifications" ];
        } );
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

        RecurringJob.AddOrUpdate<ICalculateStadiumDashboardJob>(
            "Calculate Stadium Dashboards Data",
            x => x.Calculate(),
            "0 0 * * *"
        );
        
        RecurringJob.AddOrUpdate<IClearOutdatedBookingDraftsJob>(
            "Clear Outdated Booking Drafts",
            x => x.Clear(),
            "0 4 * * *"
        );
        
        RecurringJob.AddOrUpdate<IClearOutdatedStadiumDashboardJob>(
            "Clear Outdated Stadium Dashboards",
            x => x.Clear(),
            "0 3 * * *"
        );
        
        RecurringJob.AddOrUpdate<IClearOutdatedUIMessagesJob>(
            "Clear Outdated UI Messages",
            x => x.Clear(),
            "0 2 * * *"
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