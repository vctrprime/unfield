using AutoMapper;
using StadiumEngine.Domain;
using StadiumEngine.Domain.Entities.Offers;
using StadiumEngine.Domain.Services.Facades.Offers;
using StadiumEngine.Domain.Services.Identity;
using StadiumEngine.DTO.Offers.Inventories;
using StadiumEngine.Handlers.Commands.Offers.Inventories;

namespace StadiumEngine.Handlers.Handlers.Offers.Inventories;

internal sealed class AddInventoryHandler : BaseRequestHandler<AddInventoryCommand, AddInventoryDto>
{
    private readonly IInventoryFacade _inventoryFacade;

    public AddInventoryHandler(
        IInventoryFacade inventoryFacade,
        IMapper mapper, 
        IClaimsIdentityService claimsIdentityService, 
        IUnitOfWork unitOfWork) : base(mapper, claimsIdentityService, unitOfWork)
    {
        _inventoryFacade = inventoryFacade;
    }
    
    public override async ValueTask<AddInventoryDto> Handle(AddInventoryCommand request, CancellationToken cancellationToken)
    {
        try
        {
            await UnitOfWork.BeginTransaction();
            
            var inventory = Mapper.Map<Inventory>(request);
            inventory.StadiumId = _currentStadiumId;
            inventory.UserCreatedId = _userId;

            await _inventoryFacade.AddInventory(inventory, request.Images, _legalId);
            
            await UnitOfWork.CommitTransaction();

            return new AddInventoryDto();

        }
        catch
        {
            await UnitOfWork.RollbackTransaction();
            throw;
        }
    }
}