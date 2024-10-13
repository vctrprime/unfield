using AutoMapper;
using Unfield.Common;
using Unfield.Common.Exceptions;
using Unfield.Domain.Entities.Offers;
using Unfield.Domain.Services.Core.Offers;
using Unfield.Domain.Services.Identity;
using Unfield.DTO.Offers.Inventories;
using Unfield.Queries.Offers.Inventories;

namespace Unfield.Handlers.Handlers.Offers.Inventories;

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