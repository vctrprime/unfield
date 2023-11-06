using StadiumEngine.Domain.Entities.Notifications;

namespace StadiumEngine.Domain.Repositories.Notifications;

public interface IUIMessageLastReadRepository
{
    Task<UIMessageLastRead?> GetAsync( int userId, int stadiumId );
    void Add( UIMessageLastRead messageLastRead );
    void Update( UIMessageLastRead messageLastRead );
}