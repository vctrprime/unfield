using StadiumEngine.DTO.Rates.PriceGroups;
using StadiumEngine.Commands.Rates.PriceGroups;

namespace StadiumEngine.Handlers.Facades.Rates.PriceGroups;

internal interface IUpdatePriceGroupFacade
{
    Task<UpdatePriceGroupDto> UpdateAsync( UpdatePriceGroupCommand request, int stadiumId, int userId );
}