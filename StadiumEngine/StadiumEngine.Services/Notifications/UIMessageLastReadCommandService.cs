using StadiumEngine.Domain.Entities.Notifications;
using StadiumEngine.Domain.Repositories.Notifications;
using StadiumEngine.Domain.Services.Core.Notifications;

namespace StadiumEngine.Services.Notifications;

internal class UIMessageLastReadCommandService : IUIMessageLastReadCommandService
{
    private readonly IUIMessageLastReadRepository _repository;

    public UIMessageLastReadCommandService( IUIMessageLastReadRepository repository )
    {
        _repository = repository;
    }

    public void Save( UIMessageLastRead messageLastRead )
    {
        if ( messageLastRead.Id == 0 )
        {
            _repository.Add( messageLastRead );
        }
        else
        {
            _repository.Update( messageLastRead );
        }
    }
}