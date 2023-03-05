using StadiumEngine.Domain.Entities.Rates;
using StadiumEngine.Domain.Repositories.Rates;
using StadiumEngine.Repositories.Infrastructure.Contexts;

namespace StadiumEngine.Repositories.Rates;

internal class TariffDayIntervalRepository : BaseRepository<TariffDayInterval>, ITariffDayIntervalRepository
{
    public TariffDayIntervalRepository( MainDbContext context ) : base( context )
    {
    }

    public new void Add( TariffDayInterval tariffDayInterval ) => base.Add( tariffDayInterval );

    public new void Remove( IEnumerable<TariffDayInterval> tariffDayIntervals ) => base.Remove( tariffDayIntervals );
}