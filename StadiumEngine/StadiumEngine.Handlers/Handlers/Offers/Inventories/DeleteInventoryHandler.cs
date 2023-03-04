using AutoMapper;
using StadiumEngine.Domain;
using StadiumEngine.Domain.Services.Facades.Offers;
using StadiumEngine.Domain.Services.Identity;
using StadiumEngine.DTO.Offers.Inventories;
using StadiumEngine.Handlers.Commands.Offers.Inventories;

namespace StadiumEngine.Handlers.Handlers.Offers.Inventories;

internal sealed class DeleteInventoryHandler : BaseCommandHandler<DeleteInventoryCommand, DeleteInventoryDto>
{
    private readonly IInventoryCommandFacade _inventoryFacade;
    public DeleteInventoryHandler(
        IInventoryCommandFacade inventoryFacade,
        IMapper mapper, 
        IClaimsIdentityService claimsIdentityService, 
        IUnitOfWork unitOfWork) : base(mapper, claimsIdentityService, unitOfWork)
    {
        _inventoryFacade = inventoryFacade;
    }

    protected override async ValueTask<DeleteInventoryDto> HandleCommand(DeleteInventoryCommand request, CancellationToken cancellationToken)
    {
        await _inventoryFacade.DeleteInventory(request.InventoryId, _currentStadiumId);
        return new DeleteInventoryDto();
    }
}