using Unfield.Domain.Entities.Notifications;

namespace Unfield.Domain.Services.Core.Notifications;

public interface IUIMessageLastReadQueryService
{
    Task<UIMessageLastRead> GetForUserAndStadiumAsync( int userId, int stadiumId );
}