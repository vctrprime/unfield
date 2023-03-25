using StadiumEngine.Common;
using StadiumEngine.Common.Enums.Offers;
using StadiumEngine.Common.Exceptions;
using StadiumEngine.Common.Models;
using StadiumEngine.Domain;
using StadiumEngine.Domain.Entities.Offers;
using StadiumEngine.Domain.Repositories.Offers;
using StadiumEngine.Domain.Services.Facades.Offers;
using StadiumEngine.Domain.Services.Infrastructure;

namespace StadiumEngine.Services.Facades.Offers;

internal class InventoryCommandFacade : BaseOfferCommandFacade<Inventory>, IInventoryCommandFacade
{
    private readonly IInventoryRepository _inventoryRepository;

    public InventoryCommandFacade(
        IInventoryRepository inventoryRepository,
        IOffersImageRepository imageRepository,
        IOffersSportKindRepository inventorySportKindRepository,
        IImageService imageService ) : base( imageRepository, imageService, inventorySportKindRepository )
    {
        _inventoryRepository = inventoryRepository;
    }

    protected override string ImageFolder => "inventories";

    public async Task AddInventory( Inventory inventory, List<ImageFile> images, int legalId ) =>
        await base.AddOffer( inventory, images, legalId );


    public async Task UpdateInventory( Inventory inventory, List<ImageFile> images, List<SportKind> sportKinds ) =>
        await base.UpdateOffer( inventory, images, sportKinds );

    public async Task DeleteInventory( int inventoryId, int stadiumId )
    {
        Inventory? inventory = await _inventoryRepository.Get( inventoryId, stadiumId );

        if ( inventory == null )
        {
            throw new DomainException( ErrorsKeys.InventoryNotFound );
        }

        _inventoryRepository.Remove( inventory );

        DeleteAllImagesAndSportKinds( inventory );
    }

    protected override async Task AddOffer( Inventory inventory ) => await Task.Run( () => _inventoryRepository.Add( inventory ) );

    protected override async Task UpdateOffer( Inventory inventory ) => await Task.Run( () => _inventoryRepository.Update( inventory ) );

    protected override OffersSportKind CreateSportKind( int inventoryId, int userId, SportKind sportKind ) =>
        new()
        {
            InventoryId = inventoryId,
            UserCreatedId = userId,
            SportKind = sportKind
        };

    protected override OffersImage CreateImage( int inventoryId, int userId, string path, int order ) =>
        new()
        {
            InventoryId = inventoryId,
            Path = path,
            Order = order,
            UserCreatedId = userId
        };
}