using StadiumEngine.DTO.Offers.Fields;
using StadiumEngine.Commands.Offers.Fields;

namespace StadiumEngine.Handlers.Facades.Offers.Fields;

internal interface IUpdateFieldFacade
{
    Task<UpdateFieldDto> UpdateAsync( UpdateFieldCommand request, int stadiumId, int userId );
}