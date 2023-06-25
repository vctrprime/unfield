using Microsoft.EntityFrameworkCore;
using StadiumEngine.Domain.Entities.Settings;
using StadiumEngine.Domain.Repositories.Settings;
using StadiumEngine.Repositories.Infrastructure.Contexts;

namespace StadiumEngine.Repositories.Settings;

internal class BreakRepository : BaseRepository<Break>, IBreakRepository
{
    public BreakRepository( MainDbContext context ) : base( context )
    {
    }

    public async Task<List<Break>> GetAllAsync( int stadiumId ) =>
        await Entities
            .Where( x => x.StadiumId == stadiumId && !x.IsDeleted )
            .Include( x => x.BreakFields )
            .ThenInclude( bf => bf.Field )
            .ToListAsync();
}