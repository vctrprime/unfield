namespace StadiumEngine.Domain.Services.Core.Notifications;

public interface IUIMessageLastReadCommandService
{
    Task Save( int userId, int stadiumId, int messageId );
}