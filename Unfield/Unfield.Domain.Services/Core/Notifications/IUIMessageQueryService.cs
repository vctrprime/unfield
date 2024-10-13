using Unfield.Domain.Entities.Notifications;

namespace Unfield.Domain.Services.Core.Notifications;

public interface IUIMessageQueryService
{
    Task<List<UIMessage>> GetByStadiumIdAsync( int stadiumId, int userId );
}