using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using StadiumEngine.DTO.Utils;
using StadiumEngine.Commands.Utils;
using StadiumEngine.Extranet.Infrastructure.Attributes;

namespace StadiumEngine.Extranet.Controllers.Utils;

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
    public async Task<SyncPermissionsDto> Sync( [FromRoute] SyncPermissionsCommand command )
    {
        SyncPermissionsDto syncPermissionsDto = await Mediator.Send( command );

        return syncPermissionsDto;
    }
}