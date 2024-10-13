using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Unfield.Common.Constant;
using Unfield.DTO.Accounts.Roles;
using Unfield.Commands.Accounts.Roles;
using Unfield.Extranet.Infrastructure.Attributes;

namespace Unfield.Extranet.Controllers.API.Accounts;

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
    public async Task<ToggleRolePermissionDto> Post( [FromBody] ToggleRolePermissionCommand command )
    {
        ToggleRolePermissionDto dto = await Mediator.Send( command );
        return dto;
    }
}