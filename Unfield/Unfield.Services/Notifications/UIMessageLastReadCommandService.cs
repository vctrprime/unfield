using Unfield.Domain.Entities.Notifications;
using Unfield.Domain.Repositories.Notifications;
using Unfield.Domain.Services.Core.Notifications;

namespace Unfield.Services.Notifications;

internal class UIMessageLastReadCommandService : IUIMessageLastReadCommandService
{
    private readonly IUIMessageLastReadRepository _repository;

    public UIMessageLastReadCommandService( IUIMessageLastReadRepository repository )
    {
        _repository = repository;
    }

    public async Task SaveAsync( int userId, int stadiumId, int messageId )
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