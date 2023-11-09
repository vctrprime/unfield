using Microsoft.EntityFrameworkCore;
using StadiumEngine.Domain.Entities.Notifications;
using StadiumEngine.Domain.Repositories.Notifications;
using StadiumEngine.Repositories.Infrastructure.Contexts;

namespace StadiumEngine.Repositories.Notifications;

internal class UIMessageRepository : BaseRepository<UIMessage>, IUIMessageRepository
{
    public UIMessageRepository( MainDbContext context ) : base( context )
    {
    }

    public async Task<List<UIMessage>> GetAsync( int stadiumId ) => 
        await Entities
            .Include( m => m.Texts.OrderBy( x => x.Index ) )
            .Where( m => m.StadiumId == stadiumId && DateTime.Now.AddDays( -3 ) < m.DateCreated )
            .OrderByDescending( x => x.Id )
            .ToListAsync();

    public new void Add( UIMessage message ) => base.Add( message );
}