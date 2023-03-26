using AutoMapper;
using StadiumEngine.Common;
using StadiumEngine.Common.Exceptions;
using StadiumEngine.Domain.Entities.Offers;
using StadiumEngine.Domain.Services.Facades.Offers;
using StadiumEngine.Domain.Services.Identity;
using StadiumEngine.DTO.Offers.Inventories;
using StadiumEngine.Queries.Offers.Inventories;

namespace StadiumEngine.Handlers.Handlers.Offers.Inventories;

internal sealed class GetInventoryHandler : BaseRequestHandler<GetInventoryQuery, InventoryDto>
{
    private readonly IInventoryQueryFacade _inventoryFacade;

    public GetInventoryHandler(
        IInventoryQueryFacade inventoryFacade,
        IMapper mapper,
        IClaimsIdentityService claimsIdentityService ) : base( mapper, claimsIdentityService )
    {
        _inventoryFacade = inventoryFacade;
    }

    public override async ValueTask<InventoryDto> Handle( GetInventoryQuery request,
        CancellationToken cancellationToken )
    {
        Inventory? inventory = await _inventoryFacade.GetByInventoryIdAsync( request.InventoryId, _currentStadiumId );

        if ( inventory == null )
        {
            throw new DomainException( ErrorsKeys.InventoryNotFound );
        }

        InventoryDto? inventoryDto = Mapper.Map<InventoryDto>( inventory );

        return inventoryDto;
    }
}