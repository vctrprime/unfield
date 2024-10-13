using Unfield.Domain.Entities.Notifications;

namespace Unfield.Domain.Repositories.Notifications;

public interface IUIMessageRepository
{
    Task<List<UIMessage>> GetAsync( int stadiumId );
    void Add( UIMessage message );
    Task<int> DeleteByDateAsync( DateTime date, int limit );
}