using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using StadiumEngine.Commands.Settings.Main;
using StadiumEngine.Common.Constant;
using StadiumEngine.DTO.Settings.Main;
using StadiumEngine.Extranet.Infrastructure.Attributes;
using StadiumEngine.Queries.Settings.Main;

namespace StadiumEngine.Extranet.Controllers.API.Settings;

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