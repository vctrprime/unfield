using Unfield.Domain.Entities.Rates;
using Unfield.Domain.Repositories.Rates;
using Unfield.Repositories.Infrastructure.Contexts;

namespace Unfield.Repositories.Rates;

internal class TariffDayIntervalRepository : BaseRepository<TariffDayInterval>, ITariffDayIntervalRepository
{
    public TariffDayIntervalRepository( MainDbContext context ) : base( context )
    {
    }

    public new void Add( TariffDayInterval tariffDayInterval ) => base.Add( tariffDayInterval );

    public new void Remove( IEnumerable<TariffDayInterval> tariffDayIntervals ) => base.Remove( tariffDayIntervals );
}