using Microsoft.EntityFrameworkCore;
using Unfield.Domain.Entities.Rates;
using Unfield.Domain.Repositories.Rates;
using Unfield.Repositories.Infrastructure.Contexts;

namespace Unfield.Repositories.Rates;

internal class TariffRepository : BaseRepository<Tariff>, ITariffRepository
{
    public TariffRepository( MainDbContext context ) : base( context )
    {
    }

    public async Task<List<Tariff>> GetAllAsync( int stadiumId ) =>
        await Entities
            .Include( t => t.PromoCodes )
            .Include( t => t.TariffDayIntervals )
            .ThenInclude( tdi => tdi.DayInterval )
            .Where( t => t.StadiumId == stadiumId && !t.IsDeleted ).ToListAsync();

    public async Task<Tariff?> GetAsync( int tariffId, int stadiumId ) =>
        await Entities
            .Include( t => t.PromoCodes )
            .Include( t => t.TariffDayIntervals )
            .ThenInclude( tdi => tdi.DayInterval )
            .FirstOrDefaultAsync( t => t.Id == tariffId && t.StadiumId == stadiumId && !t.IsDeleted );

    public new void Add( Tariff tariff ) => base.Add( tariff );

    public new void Update( Tariff tariff ) => base.Update( tariff );

    public new void Remove( Tariff tariff )
    {
        tariff.IsDeleted = true;
        base.Update( tariff );
    }
}