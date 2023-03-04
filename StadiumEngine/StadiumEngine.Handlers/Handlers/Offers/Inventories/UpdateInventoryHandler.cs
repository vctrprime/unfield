using AutoMapper;
using StadiumEngine.Common;
using StadiumEngine.Common.Exceptions;
using StadiumEngine.Domain;
using StadiumEngine.Domain.Services.Facades.Offers;
using StadiumEngine.Domain.Services.Identity;
using StadiumEngine.DTO.Offers.Inventories;
using StadiumEngine.Handlers.Commands.Offers.Inventories;

namespace StadiumEngine.Handlers.Handlers.Offers.Inventories;

internal sealed class UpdateInventoryHandler : BaseCommandHandler<UpdateInventoryCommand, UpdateInventoryDto>
{
    private readonly IInventoryFacade _inventoryFacade;
        
    public UpdateInventoryHandler(
        IInventoryFacade inventoryFacade,
        IMapper mapper, 
        IClaimsIdentityService claimsIdentityService, 
        IUnitOfWork unitOfWork) : base(mapper, claimsIdentityService, unitOfWork)
    {
        _inventoryFacade = inventoryFacade;
    }

    protected override async ValueTask<UpdateInventoryDto> HandleCommand(UpdateInventoryCommand request, CancellationToken cancellationToken)
    {
        var inventory = await _inventoryFacade.GetByInventoryId(request.Id, _currentStadiumId);

        if (inventory == null) throw new DomainException(ErrorsKeys.InventoryNotFound);
         
        inventory.Name = request.Name;
        inventory.Description = request.Description;
        inventory.Price = request.Price;
        inventory.Currency = request.Currency;
        inventory.Quantity = request.Quantity;
        inventory.IsActive = request.IsActive;
        inventory.UserModifiedId = _userId;

        await _inventoryFacade.UpdateInventory(inventory, request.Images, request.SportKinds);
        
        return new UpdateInventoryDto();
    }
}