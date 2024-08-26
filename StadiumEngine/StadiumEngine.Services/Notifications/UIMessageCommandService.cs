using StadiumEngine.Domain.Entities.Notifications;
using StadiumEngine.Domain.Repositories.Notifications;
using StadiumEngine.Domain.Services.Core.Notifications;

namespace StadiumEngine.Services.Notifications;

internal class UIMessageCommandService : IUIMessageCommandService
{
    private readonly IUIMessageRepository _repository;

    public UIMessageCommandService( IUIMessageRepository repository )
    {
        _repository = repository;
    }

    public void Add( UIMessage message ) => _repository.Add( message );
    public async Task<int> DeleteByDateAsync( DateTime date, int limit ) => 
        await _repository.DeleteByDateAsync( date, limit );
}