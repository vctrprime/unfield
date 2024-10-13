using Microsoft.EntityFrameworkCore;
using Unfield.Domain.Entities.Rates;
using Unfield.Domain.Repositories.Rates;
using Unfield.Repositories.Infrastructure.Contexts;

namespace Unfield.Repositories.Rates;

internal class DayIntervalRepository : BaseRepository<DayInterval>, IDayIntervalRepository
{
    public DayIntervalRepository( MainDbContext context ) : base( context )
    {
    }

    public Task<DayInterval?> GetAsync( string start, string end ) =>
        Entities.FirstOrDefaultAsync( di => di.Start == start && di.End == end );

    public new void Add( DayInterval dayInterval ) => base.Add( dayInterval );
}