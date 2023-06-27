using StadiumEngine.Common;
using StadiumEngine.Common.Exceptions;
using StadiumEngine.Domain.Entities.Offers;
using StadiumEngine.Domain.Services.Application.Offers;
using StadiumEngine.DTO.Offers.Inventories;
using StadiumEngine.Commands.Offers.Inventories;

namespace StadiumEngine.Handlers.Facades.Offers.Inventories;

internal class UpdateInventoryFacade : IUpdateInventoryFacade
{
    private readonly IInventoryCommandService _commandService;
    private readonly IInventoryQueryService _queryService;

    public UpdateInventoryFacade( IInventoryQueryService queryService, IInventoryCommandService commandService )
    {
        _queryService = queryService;
        _commandService = commandService;
    }

    public async Task<UpdateInventoryDto> UpdateAsync( UpdateInventoryCommand request, int stadiumId, int userId )
    {
        Inventory? inventory = await _queryService.GetByInventoryIdAsync( request.Id, stadiumId );

        if ( inventory == null )
        {
            throw new DomainException( ErrorsKeys.InventoryNotFound );
        }

        inventory.Name = request.Name;
        inventory.Description = request.Description;
        inventory.Price = request.Price;
        inventory.Currency = request.Currency;
        inventory.Quantity = request.Quantity;
        inventory.IsActive = request.IsActive;
        inventory.UserModifiedId = userId;

        await _commandService.UpdateInventoryAsync( inventory, request.Images, request.SportKinds );

        return new UpdateInventoryDto();
    }
}