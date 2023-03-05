using Microsoft.EntityFrameworkCore;
using StadiumEngine.Domain.Entities.Rates;
using StadiumEngine.Domain.Repositories.Rates;
using StadiumEngine.Repositories.Infrastructure.Contexts;

namespace StadiumEngine.Repositories.Rates;

internal class TariffRepository : BaseRepository<Tariff>, ITariffRepository
{
    public TariffRepository( MainDbContext context ) : base( context )
    {
    }

    public async Task<List<Tariff>> GetAll( int stadiumId ) =>
        await Entities
            .Include( t => t.TariffDayIntervals )
            .ThenInclude( tdi => tdi.DayInterval )
            .Where( t => t.StadiumId == stadiumId && !t.IsDeleted ).ToListAsync();

    public async Task<Tariff?> Get( int tariffId, int stadiumId ) =>
        await Entities
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