using StadiumEngine.Domain.Entities.Notifications;

namespace StadiumEngine.Domain.Services.Core.Notifications;

public interface IUIMessageLastReadQueryService
{
    Task<UIMessageLastRead> GetForUserAndStadiumAsync( int userId, int stadiumId );
}