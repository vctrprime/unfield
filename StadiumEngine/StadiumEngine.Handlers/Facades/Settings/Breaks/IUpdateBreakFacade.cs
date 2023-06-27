using StadiumEngine.Commands.Settings.Breaks;
using StadiumEngine.DTO.Settings.Breaks;

namespace StadiumEngine.Handlers.Facades.Settings.Breaks;

internal interface IUpdateBreakFacade
{
    Task<UpdateBreakDto> UpdateAsync( UpdateBreakCommand request, int stadiumId, int userId );
}