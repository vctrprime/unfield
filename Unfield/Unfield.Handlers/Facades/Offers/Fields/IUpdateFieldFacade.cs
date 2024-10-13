using Unfield.DTO.Offers.Fields;
using Unfield.Commands.Offers.Fields;

namespace Unfield.Handlers.Facades.Offers.Fields;

internal interface IUpdateFieldFacade
{
    Task<UpdateFieldDto> UpdateAsync( UpdateFieldCommand request, int stadiumId, int userId );
}