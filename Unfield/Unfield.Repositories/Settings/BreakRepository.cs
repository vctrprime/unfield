using Microsoft.EntityFrameworkCore;
using Unfield.Domain.Entities.Settings;
using Unfield.Domain.Repositories.Settings;
using Unfield.Repositories.Infrastructure.Contexts;

namespace Unfield.Repositories.Settings;

internal class BreakRepository : BaseRepository<Break>, IBreakRepository
{
    public BreakRepository( MainDbContext context ) : base( context )
    {
    }

    public async Task<List<Break>> GetAllAsync( int stadiumId ) =>
        await Entities
            .Where( x => x.StadiumId == stadiumId && !x.IsDeleted )
            .Include( x => x.BreakFields )
            .ToListAsync();

    public async Task<Break?> GetAsync( int breakId, int stadiumId ) =>
        await Entities
            .Include( x => x.BreakFields )
            .FirstOrDefaultAsync( x => x.Id == breakId && x.StadiumId == stadiumId && !x.IsDeleted );

    public new void Add( Break @break ) => base.Add( @break );

    public new void Update( Break @break ) => base.Update( @break );

    public new void Remove( Break @break )
    {
        @break.IsDeleted = true;
        base.Update( @break );
    }
}