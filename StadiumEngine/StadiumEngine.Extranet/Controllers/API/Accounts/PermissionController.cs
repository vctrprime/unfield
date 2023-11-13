using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using StadiumEngine.Common.Constant;
using StadiumEngine.DTO.Accounts.Permissions;
using StadiumEngine.Extranet.Infrastructure.Attributes;
using StadiumEngine.Queries.Accounts.Roles;

namespace StadiumEngine.Extranet.Controllers.API.Accounts;

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
    public async Task<List<PermissionDto>> Get( [FromRoute] GetPermissionsForRoleQuery query )
    {
        List<PermissionDto> permissions = await Mediator.Send( query );
        return permissions;
    }
}