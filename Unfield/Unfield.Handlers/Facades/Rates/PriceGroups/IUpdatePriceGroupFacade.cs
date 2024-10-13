using Unfield.DTO.Rates.PriceGroups;
using Unfield.Commands.Rates.PriceGroups;

namespace Unfield.Handlers.Facades.Rates.PriceGroups;

internal interface IUpdatePriceGroupFacade
{
    Task<UpdatePriceGroupDto> UpdateAsync( UpdatePriceGroupCommand request, int stadiumId, int userId );
}