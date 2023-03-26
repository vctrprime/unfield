using StadiumEngine.DTO.Offers.Inventories;
using StadiumEngine.Commands.Offers.Inventories;

namespace StadiumEngine.Handlers.Facades.Offers.Inventories;

internal interface IUpdateInventoryFacade
{
    Task<UpdateInventoryDto> UpdateAsync( UpdateInventoryCommand request, int stadiumId, int userId );
}