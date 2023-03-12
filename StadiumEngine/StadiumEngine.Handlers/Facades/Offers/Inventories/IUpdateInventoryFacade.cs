using StadiumEngine.DTO.Offers.Inventories;
using StadiumEngine.Commands.Offers.Inventories;

namespace StadiumEngine.Handlers.Facades.Offers.Inventories;

internal interface IUpdateInventoryFacade
{
    Task<UpdateInventoryDto> Update( UpdateInventoryCommand request, int stadiumId, int userId );
}