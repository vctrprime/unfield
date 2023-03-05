using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using StadiumEngine.Common.Constant;
using StadiumEngine.DTO.Accounts.Permissions;
using StadiumEngine.Handlers.Queries.Accounts.Roles;
using StadiumEngine.WebUI.Infrastructure.Attributes;

namespace StadiumEngine.WebUI.Controllers.API.Accounts;

/// <summary>
///     Разрешения
/// </summary>
[Route( "api/accounts/permissions" )]
public class PermissionController : BaseApiController
{
    /// <summary>
    ///     Получить разрешения для роли
    /// </summary>
    /// <returns></returns>
    [HttpGet( "{roleId}" )]
    [HasPermission( PermissionsKeys.GetPermissions )]
    public async Task<List<PermissionDto>> Get( int roleId )
    {
        var permissions = await Mediator.Send( new GetPermissionsForRoleQuery( roleId ) );
        return permissions;
    }
}