using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using StadiumEngine.Common.Constant;
using StadiumEngine.DTO.Accounts;
using StadiumEngine.DTO.Accounts.Roles;
using StadiumEngine.Handlers.Commands.Accounts;
using StadiumEngine.Handlers.Commands.Accounts.Roles;
using StadiumEngine.Handlers.Queries.Accounts;
using StadiumEngine.Handlers.Queries.Accounts.Roles;
using StadiumEngine.WebUI.Infrastructure.Attributes;

namespace StadiumEngine.WebUI.Controllers.API.Accounts;

/// <summary>
/// Роли
/// </summary>
[Route( "api/accounts/roles" )]
public class RoleController : BaseApiController
{
    /// <summary>
    /// Получить роли
    /// </summary>
    /// <returns></returns>
    [HttpGet]
    [HasPermission( $"{PermissionsKeys.GetRoles},{PermissionsKeys.GetPermissions},{PermissionsKeys.GetUsers}" )]
    public async Task<List<RoleDto>> Get()
    {
        var roles = await Mediator.Send( new GetRolesQuery() );
        return roles;
    }

    /// <summary>
    /// Добавить роль
    /// </summary>
    /// <returns></returns>
    [HttpPost]
    [HasPermission( PermissionsKeys.InsertRole )]
    public async Task<AddRoleDto> Post( AddRoleCommand command )
    {
        var dto = await Mediator.Send( command );
        return dto;
    }

    /// <summary>
    /// Обновить роль
    /// </summary>
    /// <returns></returns>
    [HttpPut]
    [HasPermission( PermissionsKeys.UpdateRole )]
    public async Task<UpdateRoleDto> Put( UpdateRoleCommand command )
    {
        var dto = await Mediator.Send( command );
        return dto;
    }

    /// <summary>
    /// Удалить роль
    /// </summary>
    /// <returns></returns>
    [HttpDelete( "{roleId}" )]
    [HasPermission( PermissionsKeys.DeleteRole )]
    public async Task<DeleteRoleDto> Delete( int roleId )
    {
        var dto = await Mediator.Send( new DeleteRoleCommand( roleId ) );
        return dto;
    }
}