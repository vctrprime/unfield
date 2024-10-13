using Microsoft.EntityFrameworkCore;
using Unfield.Domain.Entities.Rates;
using Unfield.Domain.Repositories.Rates;
using Unfield.Repositories.Infrastructure.Contexts;

namespace Unfield.Repositories.Rates;

internal class PriceRepository : BaseRepository<Price>, IPriceRepository
{
    public PriceRepository( MainDbContext context ) : base( context )
    {
    }

    public async Task<List<Price>> GetAllAsync( int stadiumId ) =>
        await GetAllAsync( new List<int> { stadiumId } );
    
    public async Task<List<Price>> GetAllAsync( List<int> stadiumsIds ) =>
        await Entities
            .Include( p => p.Field )
            .Include( p => p.TariffDayInterval )
            .ThenInclude( i => i.Tariff )
            .ThenInclude( t => t.PromoCodes )
            .Include( p => p.TariffDayInterval )
            .ThenInclude( i => i.DayInterval )
            .Where(
                p => !p.IsObsolete
                     && stadiumsIds.Contains( p.Field.StadiumId )
                     && p.Field.IsActive
                     && !p.Field.IsDeleted
                     && p.TariffDayInterval.Tariff.IsActive
                     && !p.TariffDayInterval.Tariff.IsDeleted ).ToListAsync();

    public new void Add( IEnumerable<Price> prices ) => base.Add( prices );

    public new void Remove( IEnumerable<Price> prices ) => base.Remove( prices );
    
}