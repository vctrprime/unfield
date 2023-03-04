using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using StadiumEngine.Common.Constant;
using StadiumEngine.DTO.Accounts;
using StadiumEngine.DTO.Accounts.Roles;
using StadiumEngine.Handlers.Commands.Accounts;
using StadiumEngine.Handlers.Commands.Accounts.Roles;
using StadiumEngine.WebUI.Infrastructure.Attributes;

namespace StadiumEngine.WebUI.Controllers.API.Accounts;

/// <summary>
/// Связи ролей и объектов
/// </summary>
[Route("api/accounts/role-stadium")]
public class RoleStadiumController : BaseApiController
{

    /// <summary>
    /// Добавить/убрать связь
    /// </summary>
    /// <returns></returns>
    [HttpPost]
    [HasPermission(PermissionsKeys.ToggleRoleStadium)]
    public async Task<ToggleRoleStadiumDto> Post(ToggleRoleStadiumCommand command)
    {
        var dto = await Mediator.Send(command);
        return dto;
    }
}