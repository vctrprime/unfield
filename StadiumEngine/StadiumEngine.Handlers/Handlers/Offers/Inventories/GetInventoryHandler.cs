using AutoMapper;
using StadiumEngine.Common;
using StadiumEngine.Common.Exceptions;
using StadiumEngine.Domain.Entities.Offers;
using StadiumEngine.Domain.Services.Core.Offers;
using StadiumEngine.Domain.Services.Identity;
using StadiumEngine.DTO.Offers.Inventories;
using StadiumEngine.Queries.Offers.Inventories;

namespace StadiumEngine.Handlers.Handlers.Offers.Inventories;

internal sealed class GetInventoryHandler : BaseRequestHandler<GetInventoryQuery, InventoryDto>
{
    private readonly IInventoryQueryService _queryService;

    public GetInventoryHandler(
        IInventoryQueryService queryService,
        IMapper mapper,
        IClaimsIdentityService claimsIdentityService ) : base( mapper, claimsIdentityService )
    {
        _queryService = queryService;
    }

    public override async ValueTask<InventoryDto> Handle( GetInventoryQuery request,
        CancellationToken cancellationToken )
    {
        Inventory? inventory = await _queryService.GetByInventoryIdAsync( request.InventoryId, _currentStadiumId );

        if ( inventory == null )
        {
            throw new DomainException( ErrorsKeys.InventoryNotFound );
        }

        InventoryDto? inventoryDto = Mapper.Map<InventoryDto>( inventory );

        return inventoryDto;
    }
}