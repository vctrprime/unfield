using Microsoft.EntityFrameworkCore;
using StadiumEngine.Domain.Entities.Accounts;
using StadiumEngine.Domain.Repositories.Accounts;
using StadiumEngine.Repositories.Infrastructure.Contexts;

namespace StadiumEngine.Repositories.Accounts;

internal class StadiumGroupRepository : BaseRepository<StadiumGroup>, IStadiumGroupRepository
{
    public StadiumGroupRepository( MainDbContext context ) : base( context )
    {
    }

    public async Task<List<StadiumGroup>> GetByFilterAsync( string searchString )
    {
        string searchStringLower = searchString.ToLower();

        return await Entities
            .Where(
                l => l.Name.ToLower().Contains( searchStringLower ) ||
                     l.Description.ToLower().Contains( searchStringLower ) )
            .Take( 50 )
            .OrderBy( l => l.Name )
            .Include( l => l.Users.Where( u => !u.IsDeleted && !u.IsAdmin ) )
            .Include( l => l.Stadiums.Where( s => !s.IsDeleted ) )
            .ToListAsync();
    }

    public new void Add( StadiumGroup stadiumGroup ) => base.Add( stadiumGroup );
}