#nullable enable
using StadiumEngine.Common.Enums.Offers;
using StadiumEngine.Common.Models;
using StadiumEngine.Domain.Entities.Offers;

namespace StadiumEngine.Domain.Services.Facades.Offers;

public interface IInventoryQueryFacade
{
    Task<List<Inventory>> GetByStadiumId(int stadiumId);
    Task<Inventory?> GetByInventoryId(int inventoryId, int stadiumId);
}