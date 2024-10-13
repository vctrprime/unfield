using Unfield.Commands.Settings.Breaks;
using Unfield.DTO.Settings.Breaks;

namespace Unfield.Handlers.Facades.Settings.Breaks;

internal interface IUpdateBreakFacade
{
    Task<UpdateBreakDto> UpdateAsync( UpdateBreakCommand request, int stadiumId, int userId );
}