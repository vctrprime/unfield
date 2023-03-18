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
            .Include( p => p.TariffDayInterval )
            .ThenInclude( i => i.Tariff )
            .Where(
                p => !p.IsObsolete
                     && p.Field.StadiumId == stadiumId
                     && p.Field.IsActive
                     && !p.Field.IsDeleted
                     && p.TariffDayInterval.Tariff.IsActive
                     && !p.TariffDayInterval.Tariff.IsDeleted ).ToListAsync();

    public new void Add( IEnumerable<Price> prices ) => base.Add( prices );

    public new void Remove( IEnumerable<Price> prices ) => base.Remove( prices );
    
}