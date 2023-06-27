using StadiumEngine.Domain.Entities.Offers;
using StadiumEngine.Domain.Repositories.Offers;
using StadiumEngine.Domain.Services.Application.Offers;

namespace StadiumEngine.Services.Application.Offers;

internal class InventoryQueryService : IInventoryQueryService
{
    private readonly IInventoryRepository _inventoryRepository;

    public InventoryQueryService(
        IInventoryRepository inventoryRepository )
    {
        _inventoryRepository = inventoryRepository;
    }

    public async Task<List<Inventory>> GetByStadiumIdAsync( int stadiumId ) =>
        await _inventoryRepository.GetAllAsync( stadiumId );

    public async Task<Inventory?> GetByInventoryIdAsync( int inventoryId, int stadiumId ) =>
        await _inventoryRepository.GetAsync( inventoryId, stadiumId );
}