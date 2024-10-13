namespace Unfield.Domain.Services.Core.Notifications;

public interface IUIMessageLastReadCommandService
{
    Task SaveAsync( int userId, int stadiumId, int messageId );
}