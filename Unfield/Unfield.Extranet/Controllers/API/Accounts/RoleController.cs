using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Unfield.Common.Constant;
using Unfield.DTO.Accounts.Roles;
using Unfield.Commands.Accounts.Roles;
using Unfield.Extranet.Infrastructure.Attributes;
using Unfield.Queries.Accounts.Roles;

namespace Unfield.Extranet.Controllers.API.Accounts;

/// <summary>
///     Роли
/// </summary>
[Route( "api/accounts/roles" )]
public class RoleController : BaseApiController
{
    /// <summary>
    ///     Получить роли
    /// </summary>
    /// <returns></returns>
    [HttpGet]
    [HasPermission( $"{PermissionsKeys.GetRoles},{PermissionsKeys.GetPermissions},{PermissionsKeys.GetUsers}" )]
    public async Task<List<RoleDto>> Get( [FromRoute] GetRolesQuery query)
    {
        List<RoleDto> roles = await Mediator.Send( query );
        return roles;
    }

    /// <summary>
    ///     Добавить роль
    /// </summary>
    /// <returns></returns>
    [HttpPost]
    [HasPermission( PermissionsKeys.InsertRole )]
    public async Task<AddRoleDto> Post( [FromBody] AddRoleCommand command )
    {
        AddRoleDto dto = await Mediator.Send( command );
        return dto;
    }

    /// <summary>
    ///     Обновить роль
    /// </summary>
    /// <returns></returns>
    [HttpPut]
    [HasPermission( PermissionsKeys.UpdateRole )]
    public async Task<UpdateRoleDto> Put( [FromBody] UpdateRoleCommand command )
    {
        UpdateRoleDto dto = await Mediator.Send( command );
        return dto;
    }

    /// <summary>
    ///     Удалить роль
    /// </summary>
    /// <returns></returns>
    [HttpDelete( "{roleId}" )]
    [HasPermission( PermissionsKeys.DeleteRole )]
    public async Task<DeleteRoleDto> Delete( [FromRoute] DeleteRoleCommand command )
    {
        DeleteRoleDto dto = await Mediator.Send( command );
        return dto;
    }
}