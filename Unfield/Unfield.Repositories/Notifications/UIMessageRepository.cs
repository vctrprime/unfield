using Microsoft.EntityFrameworkCore;
using Unfield.Domain.Entities.Notifications;
using Unfield.Domain.Repositories.Notifications;
using Unfield.Repositories.Infrastructure.Contexts;

namespace Unfield.Repositories.Notifications;

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
    
    public async Task<int> DeleteByDateAsync( DateTime date, int limit )
    {
        int rows = await Entities
            .Where( x => x.DateCreated < date )
            .Take( limit )
            .ExecuteDeleteAsync();

        return rows;
    }
}