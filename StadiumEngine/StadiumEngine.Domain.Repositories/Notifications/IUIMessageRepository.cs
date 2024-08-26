using StadiumEngine.Domain.Entities.Notifications;

namespace StadiumEngine.Domain.Repositories.Notifications;

public interface IUIMessageRepository
{
    Task<List<UIMessage>> GetAsync( int stadiumId );
    void Add( UIMessage message );
    Task<int> DeleteByDateAsync( DateTime date, int limit );
}