using Microsoft.EntityFrameworkCore;
using Unfield.Domain.Entities.Geo;
using Unfield.Domain.Repositories.Accounts;
using Unfield.Domain.Repositories.Geo;
using Unfield.Repositories.Infrastructure.Contexts;

namespace Unfield.Repositories.Geo;

internal class CityRepository : BaseRepository<City>, ICityRepository
{
    public CityRepository( MainDbContext context ) : base( context )
    {
    }

    public async Task<List<City>> GetAsync( string q, int take ) =>
        await Entities
            .Include( c => c.Region )
            .ThenInclude( c => c.Country )
            .Where(
                c => c.Name.ToLower().Contains( q.ToLower() ) ||
                     c.Region.Name.ToLower().Contains( q.ToLower() ) ||
                     (c.ShortName ?? String.Empty).ToLower().Contains( q.ToLower() ) ||
                     (c.Region.ShortName ?? String.Empty).ToLower().Contains( q.ToLower() )
            )
            .OrderBy( c => c.Name )
            .Take( take )
            .ToListAsync();
}