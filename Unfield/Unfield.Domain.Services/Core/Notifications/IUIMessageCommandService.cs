using Unfield.Domain.Entities.Notifications;

namespace Unfield.Domain.Services.Core.Notifications;

public interface IUIMessageCommandService
{
    void Add( UIMessage message );
    Task<int> DeleteByDateAsync( DateTime date, int limit );
}