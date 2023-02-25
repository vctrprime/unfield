#nullable enable
using StadiumEngine.Domain.Entities.Offers;

namespace StadiumEngine.Domain.Repositories.Offers;

public interface IInventoryRepository
{
    Task<List<Inventory>> GetAll(int stadiumId);
    Task<Inventory?> Get(int inventoryId, int stadiumId);
    void Add(Inventory inventory);
    void Update(Inventory inventory);
    void Remove(Inventory inventory);
}