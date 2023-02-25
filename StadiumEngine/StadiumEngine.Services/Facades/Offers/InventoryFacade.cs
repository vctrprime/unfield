using StadiumEngine.Common;
using StadiumEngine.Common.Enums.Offers;
using StadiumEngine.Common.Exceptions;
using StadiumEngine.Common.Models;
using StadiumEngine.Domain.Entities.Offers;
using StadiumEngine.Domain.Repositories.Offers;
using StadiumEngine.Domain.Services.Facades.Offers;
using StadiumEngine.Domain.Services.Infrastructure;

namespace StadiumEngine.Services.Facades.Offers;

internal class InventoryFacade : BaseOfferFacade<Inventory>, IInventoryFacade
{
    private readonly IInventoryRepository _inventoryRepository;
    protected override string ImageFolder => "inventories";
    
    public InventoryFacade(
        IInventoryRepository inventoryRepository, 
        IOffersImageRepository imageRepository, 
        IOffersSportKindRepository inventorySportKindRepository, 
        IImageService imageService) : base(imageRepository, imageService, inventorySportKindRepository)
    {
        _inventoryRepository = inventoryRepository;
    }

    public async Task<List<Inventory>> GetByStadiumId(int stadiumId)
    {
        return await _inventoryRepository.GetAll(stadiumId);
    }

    public async Task<Inventory?> GetByInventoryId(int inventoryId, int stadiumId)
    {
        return await _inventoryRepository.Get(inventoryId, stadiumId);
    }

    public async Task AddInventory(Inventory inventory, List<ImageFile> images, int legalId)
    {
        await base.AddOffer(inventory, images, legalId);
    }
    
    
    public async Task UpdateInventory(Inventory inventory, List<ImageFile> images, List<SportKind> sportKinds)
    {
        await base.UpdateOffer(inventory, images, sportKinds);
    }

    public async Task DeleteInventory(int inventoryId, int stadiumId)
    {
        var inventory = await _inventoryRepository.Get(inventoryId, stadiumId);

        if (inventory == null) throw new DomainException(ErrorsKeys.InventoryNotFound);
        
        _inventoryRepository.Remove(inventory);
        
        DeleteAllImagesAndSportKinds(inventory);
    }
    
    protected override void AddOffer(Inventory inventory) => _inventoryRepository.Add(inventory);
    protected override void UpdateOffer(Inventory inventory) => _inventoryRepository.Update(inventory);
    
    protected override OffersSportKind CreateSportKind(int inventoryId, int userId, SportKind sportKind)
    {
        return new OffersSportKind
        {
            InventoryId = inventoryId,
            UserCreatedId = userId,
            SportKind = sportKind
        };
    }
    
    protected override OffersImage CreateImage(int inventoryId, int userId, string path, int order)
    {
        return new OffersImage
        {
            InventoryId = inventoryId,
            Path = path,
            Order = order,
            UserCreatedId = userId
        };
    }
}