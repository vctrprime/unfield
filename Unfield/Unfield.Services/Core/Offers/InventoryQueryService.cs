using Unfield.Domain.Entities.Offers;
using Unfield.Domain.Repositories.Offers;
using Unfield.Domain.Services.Core.Offers;

namespace Unfield.Services.Core.Offers;

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