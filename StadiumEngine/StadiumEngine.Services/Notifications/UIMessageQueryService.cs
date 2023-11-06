using StadiumEngine.Domain.Entities.Notifications;
using StadiumEngine.Domain.Repositories.Notifications;
using StadiumEngine.Domain.Services.Core.Notifications;

namespace StadiumEngine.Services.Notifications;

internal class UIMessageQueryService : IUIMessageQueryService
{
    private readonly IUIMessageRepository _repository;

    public UIMessageQueryService( IUIMessageRepository repository )
    {
        _repository = repository;
    }

    public async Task<List<UIMessage>> GetByStadiumIdAsync( int stadiumId ) =>
        await _repository.GetAsync( stadiumId );
}