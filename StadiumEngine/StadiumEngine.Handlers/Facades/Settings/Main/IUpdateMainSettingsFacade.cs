using StadiumEngine.Commands.Settings.Main;
using StadiumEngine.DTO.Settings.Main;

namespace StadiumEngine.Handlers.Facades.Settings.Main;

internal interface IUpdateMainSettingsFacade
{
    Task<UpdateMainSettingsDto> UpdateAsync( UpdateMainSettingsCommand request, int stadiumId, int userId );
}