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

    public async Task<List<Inventory>> GetByStadiumIdAsync( int stadiumId ) =>
        await _inventoryRepository.GetAllAsync( stadiumId );

    public async Task<Inventory?> GetByInventoryIdAsync( int inventoryId, int stadiumId ) =>
        await _inventoryRepository.GetAsync( inventoryId, stadiumId );
}