using Unfield.Domain.Entities.Notifications;
using Unfield.Domain.Repositories.Notifications;
using Unfield.Domain.Services.Core.Notifications;

namespace Unfield.Services.Notifications;

internal class UIMessageLastReadQueryService : IUIMessageLastReadQueryService
{
    private readonly IUIMessageLastReadRepository _repository;

    public UIMessageLastReadQueryService( IUIMessageLastReadRepository repository )
    {
        _repository = repository;
    }


    public async Task<UIMessageLastRead> GetForUserAndStadiumAsync( int userId, int stadiumId )
    {
        UIMessageLastRead? messageLastRead = await _repository.GetAsync( userId, stadiumId );

        if ( messageLastRead == null )
        {
            return new UIMessageLastRead
            {
                UserId = userId,
                StadiumId = stadiumId
            };
        }

        return messageLastRead;
    }
}