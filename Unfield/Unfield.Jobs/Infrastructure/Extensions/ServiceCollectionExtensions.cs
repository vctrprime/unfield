using Hangfire;
using Hangfire.PostgreSql;
using Hangfire.PostgreSql.Factories;
using Microsoft.Extensions.DependencyInjection;
using Unfield.Domain.Services.Core.Dashboard;
using Unfield.Domain.Services.Core.Notifications;
using Unfield.Jobs.Background.Dashboard;
using Unfield.Jobs.Background.Notifications;
using Unfield.Jobs.Recurring.Bookings;
using Unfield.Jobs.Recurring.Dashboard;
using Unfield.Jobs.Recurring.Notifications;
using Unfield.Jobs.Services.Builders.Dashboard;

namespace Unfield.Jobs.Infrastructure.Extensions;

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