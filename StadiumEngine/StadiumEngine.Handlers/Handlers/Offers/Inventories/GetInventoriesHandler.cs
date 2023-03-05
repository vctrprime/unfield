using AutoMapper;
using StadiumEngine.Domain.Services.Facades.Offers;
using StadiumEngine.Domain.Services.Identity;
using StadiumEngine.DTO.Offers.Inventories;
using StadiumEngine.Handlers.Queries.Offers.Inventories;

namespace StadiumEngine.Handlers.Handlers.Offers.Inventories;

internal sealed class GetInventoriesHandler : BaseRequestHandler<GetInventoriesQuery, List<InventoryDto>>
{
    private readonly IInventoryQueryFacade _inventoryFacade;

    public GetInventoriesHandler(
        IInventoryQueryFacade inventoryFacade,
        IMapper mapper,
        IClaimsIdentityService claimsIdentityService ) : base( mapper, claimsIdentityService )
    {
        _inventoryFacade = inventoryFacade;
    }

    public override async ValueTask<List<InventoryDto>> Handle( GetInventoriesQuery request,
        CancellationToken cancellationToken )
    {
        var inventories = await _inventoryFacade.GetByStadiumId( _currentStadiumId );

        var inventoriesDto = Mapper.Map<List<InventoryDto>>( inventories );

        return inventoriesDto;
    }
}