using AutoMapper;
using StadiumEngine.Domain;
using StadiumEngine.Domain.Entities.Offers;
using StadiumEngine.Domain.Services.Facades.Offers;
using StadiumEngine.Domain.Services.Identity;
using StadiumEngine.DTO.Offers.Inventories;
using StadiumEngine.Handlers.Commands.Offers.Inventories;

namespace StadiumEngine.Handlers.Handlers.Offers.Inventories;

internal sealed class AddInventoryHandler : BaseCommandHandler<AddInventoryCommand, AddInventoryDto>
{
    private readonly IInventoryCommandFacade _inventoryFacade;

    public AddInventoryHandler(
        IInventoryCommandFacade inventoryFacade,
        IMapper mapper,
        IClaimsIdentityService claimsIdentityService,
        IUnitOfWork unitOfWork ) : base( mapper, claimsIdentityService, unitOfWork )
    {
        _inventoryFacade = inventoryFacade;
    }

    protected override async ValueTask<AddInventoryDto> HandleCommand( AddInventoryCommand request,
        CancellationToken cancellationToken )
    {
        Inventory? inventory = Mapper.Map<Inventory>( request );
        inventory.StadiumId = _currentStadiumId;
        inventory.UserCreatedId = _userId;

        await _inventoryFacade.AddInventory( inventory, request.Images, _legalId );

        return new AddInventoryDto();
    }
}