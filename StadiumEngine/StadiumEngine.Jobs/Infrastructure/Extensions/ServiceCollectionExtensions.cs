using Hangfire;
using Hangfire.PostgreSql;
using Hangfire.PostgreSql.Factories;
using Microsoft.Extensions.DependencyInjection;
using StadiumEngine.Domain.Services.Core.Dashboard;
using StadiumEngine.Domain.Services.Core.Notifications;
using StadiumEngine.Jobs.Background.Dashboard;
using StadiumEngine.Jobs.Background.Notifications;
using StadiumEngine.Jobs.Recurring.Bookings;
using StadiumEngine.Jobs.Recurring.Dashboard;
using StadiumEngine.Jobs.Recurring.Notifications;
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
        services.AddScoped<ICalculateStadiumDashboardJob, CalculateStadiumDashboardJob>();
        services.AddScoped<IStadiumDashboardCalculator, StadiumDashboardCalculator>();
        services.AddScoped<IDashboardQueueManager, DashboardQueueManager>();
        services.AddScoped<INotificationsQueueManager, NotificationsQueueManager>();
        services.AddScoped<IBackgroundNotificationSender, BackgroundNotificationSender>();
        
        services.AddScoped<IClearOutdatedBookingDraftsJob, ClearOutdatedBookingDraftsJob>();
        services.AddScoped<IClearOutdatedStadiumDashboardJob, ClearOutdatedStadiumDashboardJob>();
        services.AddScoped<IClearOutdatedUIMessagesJob, ClearOutdatedUIMessagesJob>();

        services.AddSingleton<IBackgroundJobClient>(
            _ => new BackgroundJobClient(
                new PostgreSqlStorage(
                    new NpgsqlConnectionFactory( connectionString, new PostgreSqlStorageOptions() ) ) ) );
    }
}