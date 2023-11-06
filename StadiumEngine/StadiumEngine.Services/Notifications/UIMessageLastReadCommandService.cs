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

    public async Task Save( int userId, int stadiumId, int messageId )
    {
        UIMessageLastRead? messageLastRead = await _repository.GetAsync( userId, stadiumId );
        if ( messageLastRead == null )
        {
            _repository.Add( new UIMessageLastRead
            {
                MessageId = messageId,
                UserId = userId,
                StadiumId = stadiumId
            } );
        }
        else
        {
            messageLastRead.MessageId = messageId;
            _repository.Update( messageLastRead );
        }
    }
}