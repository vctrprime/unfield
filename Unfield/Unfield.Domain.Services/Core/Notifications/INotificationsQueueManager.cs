using Unfield.Common.Enums.Notifications;

namespace Unfield.Domain.Services.Core.Notifications;

public interface INotificationsQueueManager
{
    void EnqueuePasswordNotification( 
        string phoneNumber,
        string password,
        string language,
        PasswordNotificationType type,
        PasswordNotificationSubject subject,
        string? stadiumGroupName = null );

    void EnqueueBookingConfirmedNotification( string bookingNumber );
    void EnqueueBookingCanceledNotification( string bookingNumber, DateTime day, string? reason );
}