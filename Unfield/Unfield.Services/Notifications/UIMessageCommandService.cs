using Unfield.Domain.Entities.Notifications;
using Unfield.Domain.Repositories.Notifications;
using Unfield.Domain.Services.Core.Notifications;

namespace Unfield.Services.Notifications;

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