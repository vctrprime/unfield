using AutoMapper;
using StadiumEngine.Domain;
using StadiumEngine.Domain.Entities.Offers;
using StadiumEngine.Domain.Services.Facades.Offers;
using StadiumEngine.Domain.Services.Identity;
using StadiumEngine.DTO.Offers.Inventories;
using StadiumEngine.Commands.Offers.Inventories;

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

    protected override async ValueTask<AddInventoryDto> HandleCommandAsync( AddInventoryCommand request,
        CancellationToken cancellationToken )
    {
        Inventory? inventory = Mapper.Map<Inventory>( request );
        inventory.StadiumId = _currentStadiumId;
        inventory.UserCreatedId = _userId;

        await _inventoryFacade.AddInventoryAsync( inventory, request.Images, _legalId );

        return new AddInventoryDto();
    }
}