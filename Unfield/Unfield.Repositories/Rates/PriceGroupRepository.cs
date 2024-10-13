using Microsoft.EntityFrameworkCore;
using Unfield.Domain.Entities.Rates;
using Unfield.Domain.Repositories.Rates;
using Unfield.Repositories.Infrastructure.Contexts;

namespace Unfield.Repositories.Rates;

internal class PriceGroupRepository : BaseRepository<PriceGroup>, IPriceGroupRepository
{
    public PriceGroupRepository( MainDbContext context ) : base( context )
    {
    }

    public async Task<List<PriceGroup>> GetAllAsync( int stadiumId ) =>
        await Entities
            .Include( pg => pg.Fields.Where( f => f.IsActive && !f.IsDeleted ) )
            .Where( pg => pg.StadiumId == stadiumId && !pg.IsDeleted ).ToListAsync();

    public async Task<PriceGroup?> GetAsync( int priceGroupId, int stadiumId ) =>
        await Entities
            .Include( pg => pg.Fields.Where( f => f.IsActive && !f.IsDeleted ) )
            .FirstOrDefaultAsync( pg => pg.Id == priceGroupId && pg.StadiumId == stadiumId && !pg.IsDeleted );

    public new void Add( PriceGroup priceGroup ) => base.Add( priceGroup );

    public new void Update( PriceGroup priceGroup ) => base.Update( priceGroup );

    public new void Remove( PriceGroup priceGroup )
    {
        priceGroup.IsDeleted = true;
        base.Update( priceGroup );
    }
}