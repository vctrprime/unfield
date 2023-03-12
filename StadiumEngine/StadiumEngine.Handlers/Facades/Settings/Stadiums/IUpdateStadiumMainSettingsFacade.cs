using StadiumEngine.DTO.Settings.Stadiums;
using StadiumEngine.Handlers.Commands.Settings.Stadiums;

namespace StadiumEngine.Handlers.Facades.Settings.Stadiums;

internal interface IUpdateStadiumMainSettingsFacade
{
    Task<UpdateStadiumMainSettingsDto> Update( UpdateStadiumMainSettingsCommand request, int stadiumId, int userId );
}