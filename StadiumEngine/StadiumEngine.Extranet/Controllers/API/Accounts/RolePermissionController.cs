using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using StadiumEngine.Common.Constant;
using StadiumEngine.DTO.Accounts.Roles;
using StadiumEngine.Commands.Accounts.Roles;
using StadiumEngine.Extranet.Infrastructure.Attributes;

namespace StadiumEngine.Extranet.Controllers.API.Accounts;

/// <summary>
///     Связи ролей и разрешений
/// </summary>
[Route( "api/accounts/role-permission" )]
public class RolePermissionController : BaseApiController
{
    /// <summary>
    ///     Добавить/убрать связь
    /// </summary>
    /// <returns></returns>
    [HttpPost]
    [HasPermission( PermissionsKeys.ToggleRolePermission )]
    public async Task<ToggleRolePermissionDto> Post( ToggleRolePermissionCommand command )
    {
        ToggleRolePermissionDto dto = await Mediator.Send( command );
        return dto;
    }
}