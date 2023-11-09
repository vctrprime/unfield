namespace StadiumEngine.Domain.Services.Core.Notifications;

public interface IUINotificationService
{
    Task Notify( IUIMessageBuilder builder );
}