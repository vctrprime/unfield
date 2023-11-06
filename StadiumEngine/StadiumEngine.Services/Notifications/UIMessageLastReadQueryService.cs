using StadiumEngine.Domain.Entities.Notifications;
using StadiumEngine.Domain.Repositories.Notifications;
using StadiumEngine.Domain.Services.Core.Notifications;

namespace StadiumEngine.Services.Notifications;

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