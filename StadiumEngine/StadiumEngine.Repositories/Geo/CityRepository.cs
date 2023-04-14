using Microsoft.EntityFrameworkCore;
using StadiumEngine.Domain.Entities.Geo;
using StadiumEngine.Domain.Repositories.Accounts;
using StadiumEngine.Domain.Repositories.Geo;
using StadiumEngine.Repositories.Infrastructure.Contexts;

namespace StadiumEngine.Repositories.Geo;

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