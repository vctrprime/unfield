using StadiumEngine.Domain.Entities.Notifications;

namespace StadiumEngine.Domain.Services.Core.Notifications;

public interface IUIMessageLastReadCommandService
{
    void Save( UIMessageLastRead messageLastRead );
}