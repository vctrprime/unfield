using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using StadiumEngine.Commands.Settings.Main;
using StadiumEngine.Common.Constant;
using StadiumEngine.DTO.Settings.Main;
using StadiumEngine.Queries.Settings.Main;
using StadiumEngine.WebUI.Infrastructure.Attributes;

namespace StadiumEngine.WebUI.Controllers.API.Settings;

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
    public async Task<MainSettingsDto> Get()
    {
        MainSettingsDto settings = await Mediator.Send( new GetMainSettingsQuery() );
        return settings;
    }
    
    /// <summary>
    ///     Обновить основные настройки
    /// </summary>
    /// <returns></returns>
    [HttpPut]
    [HasPermission( PermissionsKeys.UpdateMainSettings )]
    public async Task<UpdateMainSettingsDto> Put( UpdateMainSettingsCommand command )
    {
        UpdateMainSettingsDto dto = await Mediator.Send( command );
        return dto;
    }
    
}