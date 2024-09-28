using Hangfire;
using StadiumEngine.Common.Enums.Notifications;
using StadiumEngine.Domain.Services.Core.Notifications;
using StadiumEngine.Domain.Services.Infrastructure;
using StadiumEngine.Domain.Services.Models.Notifications;

namespace StadiumEngine.Jobs.Background.Notifications;

internal class NotificationsQueueManager : INotificationsQueueManager
{
    private readonly IBackgroundJobClient _backgroundJobClient;
    private readonly IBackgroundNotificationSender _backgroundNotificationSender;

    public NotificationsQueueManager(
        IBackgroundJobClient backgroundJobClient,
        IBackgroundNotificationSender backgroundNotificationSender )
    {
        _backgroundJobClient = backgroundJobClient;
        _backgroundNotificationSender = backgroundNotificationSender;
    }

    public void EnqueuePasswordNotification(
        string phoneNumber,
        string password,
        string language,
        PasswordNotificationType type,
        PasswordNotificationSubject subject,
        string? stadiumGroupName = null ) =>
        _backgroundJobClient.Enqueue(
            () => _backgroundNotificationSender.SendPasswordAsync(
                phoneNumber,
                password,
                language,
                type,
                subject,
                stadiumGroupName ) );

    public void EnqueueBookingConfirmedNotification( string bookingNumber ) => 
        _backgroundJobClient.Enqueue(
        () => _backgroundNotificationSender.SendBookingConfirmedAsync( bookingNumber ) );

    public void EnqueueBookingCanceledNotification( string bookingNumber, DateTime day, string? reason ) =>
        _backgroundJobClient.Enqueue(
            () => _backgroundNotificationSender.SendBookingCanceledAsync( bookingNumber, day, reason ) );
}