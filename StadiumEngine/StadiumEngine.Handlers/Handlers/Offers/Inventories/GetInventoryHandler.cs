using AutoMapper;
using StadiumEngine.Common;
using StadiumEngine.Common.Exceptions;
using StadiumEngine.Domain;
using StadiumEngine.Domain.Services.Facades.Offers;
using StadiumEngine.Domain.Services.Identity;
using StadiumEngine.DTO.Offers.Inventories;
using StadiumEngine.Handlers.Queries.Offers.Inventories;

namespace StadiumEngine.Handlers.Handlers.Offers.Inventories;

internal sealed class GetInventoryHandler : BaseRequestHandler<GetInventoryQuery, InventoryDto>
{
    private readonly IInventoryFacade _inventoryFacade;

    public GetInventoryHandler(
        IInventoryFacade inventoryFacade, 
        IMapper mapper, 
        IClaimsIdentityService claimsIdentityService) : base(mapper, claimsIdentityService)
    {
        _inventoryFacade = inventoryFacade;
    }
    
    public override async ValueTask<InventoryDto> Handle(GetInventoryQuery request, CancellationToken cancellationToken)
    {
        var inventory = await _inventoryFacade.GetByInventoryId(request.InventoryId, _currentStadiumId);

        if (inventory == null) throw new DomainException(ErrorsKeys.InventoryNotFound);

        var inventoryDto = Mapper.Map<InventoryDto>(inventory);

        return inventoryDto;
    }
}