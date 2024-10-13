using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Unfield.Common.Constant;
using Unfield.DTO.Accounts.Permissions;
using Unfield.Extranet.Infrastructure.Attributes;
using Unfield.Queries.Accounts.Roles;

namespace Unfield.Extranet.Controllers.API.Accounts;

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