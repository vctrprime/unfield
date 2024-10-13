namespace Unfield.Domain.Services.Core.Notifications;

public interface IUINotificationService
{
    Task Notify( IUIMessageBuilder builder );
}