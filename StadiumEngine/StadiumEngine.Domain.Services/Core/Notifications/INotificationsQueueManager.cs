using StadiumEngine.Common.Enums.Notifications;

namespace StadiumEngine.Domain.Services.Core.Notifications;

public interface INotificationsQueueManager
{
    void EnqueuePasswordNotification( 
        string phoneNumber,
        string password,
        string language,
        PasswordNotificationType type,
        PasswordNotificationSubject subject,
        string? stadiumGroupName = null );
}