using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Unfield.Commands.Settings.Main;
using Unfield.Common.Constant;
using Unfield.DTO.Settings.Main;
using Unfield.Extranet.Infrastructure.Attributes;
using Unfield.Queries.Settings.Main;

namespace Unfield.Extranet.Controllers.API.Settings;

/// <summary>
///     Основные настройки
/// </summary>
[Route( "api/settings/main" )]
public class MainSettingsController : BaseApiController
{
    /// <summary>
    ///     Получить основные настройки
    /// </summary>
    /// <returns></returns>
    [HttpGet]
    public async Task<MainSettingsDto> Get( [FromQuery] GetMainSettingsQuery query )
    {
        MainSettingsDto settings = await Mediator.Send( query );
        return settings;
    }
    
    /// <summary>
    ///     Обновить основные настройки
    /// </summary>
    /// <returns></returns>
    [HttpPut]
    [HasPermission( PermissionsKeys.UpdateMainSettings )]
    public async Task<UpdateMainSettingsDto> Put(  [FromBody] UpdateMainSettingsCommand command )
    {
        UpdateMainSettingsDto dto = await Mediator.Send( command );
        return dto;
    }
    
}