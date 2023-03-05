using StadiumEngine.DTO.Offers.Fields;
using StadiumEngine.Handlers.Commands.Offers.Fields;

namespace StadiumEngine.Handlers.Facades.Offers.Fields;

internal interface IUpdateFieldFacade
{
    Task<UpdateFieldDto> Update( UpdateFieldCommand request, int stadiumId, int userId );
}