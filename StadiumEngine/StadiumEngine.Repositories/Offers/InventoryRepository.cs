using Microsoft.EntityFrameworkCore;
using StadiumEngine.Domain.Entities.Offers;
using StadiumEngine.Domain.Repositories.Offers;
using StadiumEngine.Repositories.Infrastructure.Contexts;

namespace StadiumEngine.Repositories.Offers;

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