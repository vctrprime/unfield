using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using StadiumEngine.DTO.Utils;
using StadiumEngine.Handlers.Commands.Utils;
using StadiumEngine.WebUI.Infrastructure.Attributes;

namespace StadiumEngine.WebUI.Controllers.Utils;

/// <summary>
///     Util-запросы для работы с разрешениями
/// </summary>
[Route( "utils/permissions" )]
[AllowAnonymous]
public class PermissionUtilController : BaseApiController
{
    /// <summary>
    ///     Синхронизировать разрешения
    /// </summary>
    /// <returns></returns>
    [HttpPost( "sync" )]
    [SecuredUtil]
    public async Task<SyncPermissionsDto> Sync()
    {
        SyncPermissionsDto syncPermissionsDto = await Mediator.Send( new SyncPermissionsCommand() );

        return syncPermissionsDto;
    }
}