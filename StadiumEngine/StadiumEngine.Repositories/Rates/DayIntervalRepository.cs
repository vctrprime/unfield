using Microsoft.EntityFrameworkCore;
using StadiumEngine.Domain.Entities.Rates;
using StadiumEngine.Domain.Repositories.Rates;
using StadiumEngine.Repositories.Infrastructure.Contexts;

namespace StadiumEngine.Repositories.Rates;

internal class DayIntervalRepository : BaseRepository<DayInterval>, IDayIntervalRepository
{
    public DayIntervalRepository( MainDbContext context ) : base( context )
    {
    }

    public Task<DayInterval?> GetAsync( string start, string end ) =>
        Entities.FirstOrDefaultAsync( di => di.Start == start && di.End == end );

    public new void Add( DayInterval dayInterval ) => base.Add( dayInterval );
}