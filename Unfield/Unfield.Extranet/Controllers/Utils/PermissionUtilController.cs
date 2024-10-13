using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Unfield.DTO.Utils;
using Unfield.Commands.Utils;
using Unfield.Extranet.Infrastructure.Attributes;

namespace Unfield.Extranet.Controllers.Utils;

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