using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using StadiumEngine.Common.Constant;
using StadiumEngine.DTO.Settings.Stadiums;
using StadiumEngine.Commands.Settings.Stadiums;
using StadiumEngine.Queries.Settings.Stadiums;
using StadiumEngine.WebUI.Infrastructure.Attributes;

namespace StadiumEngine.WebUI.Controllers.API.Settings;

/// <summary>
///     Основные настройки
/// </summary>
[Route( "api/settings/main" )]
public class StadiumMainSettingsController : BaseApiController
{
    /// <summary>
    ///     Получить основные настройки
    /// </summary>
    /// <returns></returns>
    [HttpGet]
    public async Task<StadiumMainSettingsDto> Get()
    {
        StadiumMainSettingsDto settings = await Mediator.Send( new GetStadiumMainSettingsQuery() );
        return settings;
    }
    
    /// <summary>
    ///     Обновить основные настройки
    /// </summary>
    /// <returns></returns>
    [HttpPut]
    [HasPermission( PermissionsKeys.UpdateMainSettings )]
    public async Task<UpdateStadiumMainSettingsDto> Put( UpdateStadiumMainSettingsCommand command )
    {
        UpdateStadiumMainSettingsDto dto = await Mediator.Send( command );
        return dto;
    }
    
}