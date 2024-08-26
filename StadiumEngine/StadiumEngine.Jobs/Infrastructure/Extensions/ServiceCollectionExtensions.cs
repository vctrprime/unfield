using Hangfire;
using Hangfire.PostgreSql;
using Hangfire.PostgreSql.Factories;
using Microsoft.Extensions.DependencyInjection;
using StadiumEngine.Jobs.Background.Dashboard;
using StadiumEngine.Jobs.Recurring.Bookings;
using StadiumEngine.Jobs.Recurring.Dashboard;
using StadiumEngine.Jobs.Services.Builders.Dashboard;

namespace StadiumEngine.Jobs.Infrastructure.Extensions;

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
    public static void RegisterJobs(
        this IServiceCollection services,
        string connectionString )
    {
        services.AddScoped<IStadiumDashboardDataBuilder, StadiumDashboardDataBuilder>();
        services.AddScoped<IDashboardCalculatorJob, DashboardCalculatorJob>();
        services.AddScoped<IStadiumDashboardCalculator, StadiumDashboardCalculator>();
        services.AddScoped<IDashboardQueueManager, DashboardQueueManager>();
        
        services.AddScoped<IClearOutdatedBookingDraftsJob, ClearOutdatedBookingDraftsJob>();

        services.AddSingleton<IBackgroundJobClient>(
            _ => new BackgroundJobClient(
                new PostgreSqlStorage(
                    new NpgsqlConnectionFactory( connectionString, new PostgreSqlStorageOptions() ) ) ) );
    }
}