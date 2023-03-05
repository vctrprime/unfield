using Microsoft.EntityFrameworkCore;
using StadiumEngine.Domain.Entities.Accounts;
using StadiumEngine.Domain.Repositories.Accounts;
using StadiumEngine.Repositories.Infrastructure.Contexts;

namespace StadiumEngine.Repositories.Accounts;

internal class LegalRepository : BaseRepository<Legal>, ILegalRepository
{
    public LegalRepository( MainDbContext context ) : base( context )
    {
    }

    public async Task<List<Legal>> GetByFilter( string searchString )
    {
        var searchStringLower = searchString.ToLower();

        return await Entities
            .Where(
                l => l.Name.ToLower().Contains( searchStringLower ) ||
                     l.Inn.ToLower().Contains( searchStringLower ) ||
                     l.Description.ToLower().Contains( searchStringLower ) ||
                     l.HeadName.ToLower().Contains( searchStringLower ) )
            .Take( 50 )
            .OrderBy( l => l.Name )
            .Include( l => l.City ).ThenInclude( c => c.Region ).ThenInclude( r => r.Country )
            .Include( l => l.Users.Where( u => !u.IsDeleted && !u.IsAdmin ) )
            .Include( l => l.Stadiums.Where( s => !s.IsDeleted ) )
            .ToListAsync();
    }

    public new void Add( Legal legal )
    {
        base.Add( legal );
    }
}