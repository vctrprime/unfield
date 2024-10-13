using Unfield.Domain.Entities.Notifications;

namespace Unfield.Domain.Repositories.Notifications;

public interface IUIMessageLastReadRepository
{
    Task<UIMessageLastRead?> GetAsync( int userId, int stadiumId );
    void Add( UIMessageLastRead messageLastRead );
    void Update( UIMessageLastRead messageLastRead );
}