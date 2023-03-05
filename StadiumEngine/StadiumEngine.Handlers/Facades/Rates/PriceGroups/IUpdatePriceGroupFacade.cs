using StadiumEngine.DTO.Rates.PriceGroups;
using StadiumEngine.Handlers.Commands.Rates.PriceGroups;

namespace StadiumEngine.Handlers.Facades.Rates.PriceGroups;

internal interface IUpdatePriceGroupFacade
{
    Task<UpdatePriceGroupDto> Update( UpdatePriceGroupCommand request, int stadiumId, int userId );
}