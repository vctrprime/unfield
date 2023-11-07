using StadiumEngine.Domain.Entities.Notifications;

namespace StadiumEngine.Domain.Services.Core.Notifications;

public interface IUIMessageQueryService
{
    Task<List<UIMessage>> GetByStadiumIdAsync( int stadiumId, int userId );
}