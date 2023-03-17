using Microsoft.EntityFrameworkCore;
using StadiumEngine.Domain.Entities.Rates;
using StadiumEngine.Domain.Repositories.Rates;
using StadiumEngine.Repositories.Infrastructure.Contexts;

namespace StadiumEngine.Repositories.Rates;

internal class PriceRepository : BaseRepository<Price>, IPriceRepository
{
    public PriceRepository( MainDbContext context ) : base( context )
    {
    }

    public async Task<List<Price>> GetAll( int stadiumId ) =>
        await Entities
            .Include( p => p.Field )
            .Where( p => !p.IsObsolete && p.Field.StadiumId == stadiumId ).ToListAsync();

    public new void Add( IEnumerable<Price> prices ) => base.Add( prices );

    public new void Remove( IEnumerable<Price> prices )
    {
        foreach (Price price in prices)
        {
            price.IsObsolete = true;
            Update( price );
        }
    }
}