using Unfield.Common;
using Unfield.Common.Exceptions;
using Unfield.Domain.Entities.Offers;
using Unfield.Domain.Services.Core.Offers;
using Unfield.DTO.Offers.Inventories;
using Unfield.Commands.Offers.Inventories;

namespace Unfield.Handlers.Facades.Offers.Inventories;

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