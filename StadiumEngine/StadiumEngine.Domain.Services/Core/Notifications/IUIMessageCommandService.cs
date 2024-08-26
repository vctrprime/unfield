using StadiumEngine.Domain.Entities.Notifications;

namespace StadiumEngine.Domain.Services.Core.Notifications;

public interface IUIMessageCommandService
{
    void Add( UIMessage message );
    Task<int> DeleteByDateAsync( DateTime date, int limit );
}