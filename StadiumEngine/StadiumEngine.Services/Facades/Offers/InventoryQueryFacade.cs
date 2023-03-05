using StadiumEngine.Domain.Entities.Offers;
using StadiumEngine.Domain.Repositories.Offers;
using StadiumEngine.Domain.Services.Facades.Offers;

namespace StadiumEngine.Services.Facades.Offers;

internal class InventoryQueryFacade : IInventoryQueryFacade
{
    private readonly IInventoryRepository _inventoryRepository;

    public InventoryQueryFacade(
        IInventoryRepository inventoryRepository )
    {
        _inventoryRepository = inventoryRepository;
    }

    public async Task<List<Inventory>> GetByStadiumId( int stadiumId )
    {
        return await _inventoryRepository.GetAll( stadiumId );
    }

    public async Task<Inventory?> GetByInventoryId( int inventoryId, int stadiumId )
    {
        return await _inventoryRepository.Get( inventoryId, stadiumId );
    }
}