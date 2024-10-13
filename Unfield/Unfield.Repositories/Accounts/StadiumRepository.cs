using Microsoft.EntityFrameworkCore;
using Unfield.Domain.Entities.Accounts;
using Unfield.Domain.Repositories.Accounts;
using Unfield.Repositories.Infrastructure.Contexts;

namespace Unfield.Repositories.Accounts;

internal class StadiumRepository : BaseRepository<Stadium>, IStadiumRepository
{
    public StadiumRepository( MainDbContext context ) : base( context )
    {
    }

    public async Task<List<Stadium>> GetAsync( int skip, int take ) =>
        await Entities
            .Where( x => !x.IsDeleted )
            .OrderBy( x => x.Id )
            .Skip( skip )
            .Take( take )
            .ToListAsync();

    public async Task<List<Stadium>> GetForStadiumGroupAsync( int stadiumGroupId ) =>
        await Entities
            .Where( s => s.StadiumGroupId == stadiumGroupId && !s.IsDeleted )
            .Include( s => s.City )
            .ThenInclude( c => c.Region )
            .ThenInclude( r => r.Country )
            .ToListAsync();

    public async Task<List<Stadium>> GetForUserAsync( int userId ) =>
        await Entities
            .Where( s => s.UserStadiums.Select( rs => rs.UserId ).Contains( userId ) && !s.IsDeleted )
            .Include( s => s.City )
            .ThenInclude( c => c.Region )
            .ThenInclude( r => r.Country )
            .ToListAsync();

    public async Task<Stadium?> GetByTokenAsync( string token ) =>
        await Entities
            .Include( x => x.StadiumGroup )
            .FirstOrDefaultAsync( s => s.Token == token );
}