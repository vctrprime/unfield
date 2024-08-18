using Hangfire;
using Hangfire.PostgreSql;
using StadiumEngine.BackgroundWorker.Builders.Dashboard;
using StadiumEngine.BackgroundWorker.Jobs.Dashboard;
using StadiumEngine.Services.Extensions;

namespace StadiumEngine.BackgroundWorker.Infrastructure.Extensions;

/// <summary>
///     Расширение для регистрации зависимостей
/// </summary>
public static class ServiceCollectionExtensions
{
    /// <summary>
    ///     Зарегистрировать все необходимые модули
    /// </summary>
    /// <param name="services"></param>
    /// <param name="connectionString"></param>
    /// <param name="archiveConnectionString"></param>
    public static void RegisterModules( 
        this IServiceCollection services, 
        string connectionString,
        string archiveConnectionString )
    {
        services.AddHangfire(
            configuration => configuration
                .UseSimpleAssemblyNameTypeSerializer()
                .UseRecommendedSerializerSettings()
                .UsePostgreSqlStorage( connectionString ) );
        services.AddHangfireServer();

        services.AddScoped<IStadiumDashboardDataBuilder, StadiumDashboardDataBuilder>();
        services.AddScoped<IDashboardCalculatorJob, DashboardCalculatorJob>();

        services.RegisterServices( connectionString, archiveConnectionString );
    }
}