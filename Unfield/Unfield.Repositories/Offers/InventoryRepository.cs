using Microsoft.EntityFrameworkCore;
using Unfield.Domain.Entities.Offers;
using Unfield.Domain.Repositories.Offers;
using Unfield.Repositories.Infrastructure.Contexts;

namespace Unfield.Repositories.Offers;

internal class InventoryRepository : BaseRepository<Inventory>, IInventoryRepository
{
    public InventoryRepository( MainDbContext context ) : base( context )
    {
    }

    public async Task<List<Inventory>> GetAllAsync( int stadiumId ) =>
        await Entities
            .Where( f => f.StadiumId == stadiumId && !f.IsDeleted )
            .Include( f => f.SportKinds )
            .Include( f => f.Images )
            .ToListAsync();

    public async Task<Inventory?> GetAsync( int inventoryId, int stadiumId ) =>
        await Entities
            .Include( f => f.Stadium )
            .Include( f => f.SportKinds )
            .Include( f => f.Images )
            .FirstOrDefaultAsync( f => f.Id == inventoryId && f.StadiumId == stadiumId && !f.IsDeleted );

    public new void Add( Inventory inventory ) => base.Add( inventory );

    public new void Update( Inventory inventory ) => base.Update( inventory );

    public new void Remove( Inventory inventory )
    {
        inventory.IsDeleted = true;
        base.Update( inventory );
    }
}