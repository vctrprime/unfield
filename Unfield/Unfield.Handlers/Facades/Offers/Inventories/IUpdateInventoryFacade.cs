using Unfield.DTO.Offers.Inventories;
using Unfield.Commands.Offers.Inventories;

namespace Unfield.Handlers.Facades.Offers.Inventories;

internal interface IUpdateInventoryFacade
{
    Task<UpdateInventoryDto> UpdateAsync( UpdateInventoryCommand request, int stadiumId, int userId );
}