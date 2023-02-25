using AutoMapper;
using StadiumEngine.Domain;
using StadiumEngine.Domain.Services.Facades.Offers;
using StadiumEngine.Domain.Services.Identity;
using StadiumEngine.DTO.Offers.Inventories;
using StadiumEngine.Handlers.Commands.Offers.Inventories;

namespace StadiumEngine.Handlers.Handlers.Offers.Inventories;

internal sealed class DeleteInventoryHandler : BaseRequestHandler<DeleteInventoryCommand, DeleteInventoryDto>
{
    private readonly IInventoryFacade _inventoryFacade;
    public DeleteInventoryHandler(
        IInventoryFacade inventoryFacade,
        IMapper mapper, 
        IClaimsIdentityService claimsIdentityService, 
        IUnitOfWork unitOfWork) : base(mapper, claimsIdentityService, unitOfWork)
    {
        _inventoryFacade = inventoryFacade;
    }

    public override async ValueTask<DeleteInventoryDto> Handle(DeleteInventoryCommand request, CancellationToken cancellationToken)
    {
        try
        {
            await UnitOfWork.BeginTransaction();
            
            await _inventoryFacade.DeleteInventory(request.InventoryId, _currentStadiumId);
            
            await UnitOfWork.CommitTransaction();
        }
        catch
        {
            await UnitOfWork.RollbackTransaction();
            throw;
        }

        return new DeleteInventoryDto();
    }
}