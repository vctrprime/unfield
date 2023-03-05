using StadiumEngine.Common;
using StadiumEngine.Common.Exceptions;
using StadiumEngine.Domain.Services.Facades.Offers;
using StadiumEngine.DTO.Offers.Inventories;
using StadiumEngine.Handlers.Commands.Offers.Inventories;

namespace StadiumEngine.Handlers.Facades.Offers.Inventories;

internal class UpdateInventoryFacade : IUpdateInventoryFacade
{
    private readonly IInventoryCommandFacade _commandFacade;
    private readonly IInventoryQueryFacade _queryFacade;

    public UpdateInventoryFacade( IInventoryQueryFacade queryFacade, IInventoryCommandFacade commandFacade )
    {
        _queryFacade = queryFacade;
        _commandFacade = commandFacade;
    }

    public async Task<UpdateInventoryDto> Update( UpdateInventoryCommand request, int stadiumId, int userId )
    {
        var inventory = await _queryFacade.GetByInventoryId( request.Id, stadiumId );

        if (inventory == null) throw new DomainException( ErrorsKeys.InventoryNotFound );

        inventory.Name = request.Name;
        inventory.Description = request.Description;
        inventory.Price = request.Price;
        inventory.Currency = request.Currency;
        inventory.Quantity = request.Quantity;
        inventory.IsActive = request.IsActive;
        inventory.UserModifiedId = userId;

        await _commandFacade.UpdateInventory( inventory, request.Images, request.SportKinds );

        return new UpdateInventoryDto();
    }
}