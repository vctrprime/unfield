using Microsoft.EntityFrameworkCore;
using Unfield.Domain.Entities.Notifications;
using Unfield.Domain.Repositories.Notifications;
using Unfield.Repositories.Infrastructure.Contexts;

namespace Unfield.Repositories.Notifications;

internal class UIMessageLastReadRepository : BaseRepository<UIMessageLastRead>, IUIMessageLastReadRepository
{
    public UIMessageLastReadRepository( MainDbContext context ) : base( context )
    {
    }

    public async Task<UIMessageLastRead?> GetAsync( int userId, int stadiumId ) =>
        await Entities.SingleOrDefaultAsync( m => m.UserId == userId && m.StadiumId == stadiumId );

    public new void Add( UIMessageLastRead messageLastRead ) => base.Add( messageLastRead );

    public new void Update( UIMessageLastRead messageLastRead ) => base.Update( messageLastRead );
}