using AutoMapper;
using StadiumEngine.Domain.Entities.Offers;
using StadiumEngine.Domain.Services.Core.Offers;
using StadiumEngine.Domain.Services.Identity;
using StadiumEngine.DTO.Offers.Inventories;
using StadiumEngine.Queries.Offers.Inventories;

namespace StadiumEngine.Handlers.Handlers.Offers.Inventories;

internal sealed class GetInventoriesHandler : BaseRequestHandler<GetInventoriesQuery, List<InventoryDto>>
{
    private readonly IInventoryQueryService _queryService;

    public GetInventoriesHandler(
        IInventoryQueryService queryService,
        IMapper mapper,
        IClaimsIdentityService claimsIdentityService ) : base( mapper, claimsIdentityService )
    {
        _queryService = queryService;
    }

    public override async ValueTask<List<InventoryDto>> Handle( GetInventoriesQuery request,
        CancellationToken cancellationToken )
    {
        List<Inventory> inventories = await _queryService.GetByStadiumIdAsync( _currentStadiumId );

        List<InventoryDto>? inventoriesDto = Mapper.Map<List<InventoryDto>>( inventories );

        return inventoriesDto;
    }
}