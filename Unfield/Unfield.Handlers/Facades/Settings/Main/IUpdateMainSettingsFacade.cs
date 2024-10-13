using Unfield.Commands.Settings.Main;
using Unfield.DTO.Settings.Main;

namespace Unfield.Handlers.Facades.Settings.Main;

internal interface IUpdateMainSettingsFacade
{
    Task<UpdateMainSettingsDto> UpdateAsync( UpdateMainSettingsCommand request, int stadiumId, int userId );
}