using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
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
[Route("api/accounts/roles")]
public class RoleController : BaseApiController
{
    /// <summary>
    /// Получить роли
    /// </summary>
    /// <returns></returns>
    [HttpGet]
    [HasPermission("get-roles,get-permissions,get-users")]
    public async Task<List<RoleDto>> Get()
    {
        var roles = await Mediator.Send(new GetRolesQuery());
        return roles;
    }
    
    /// <summary>
    /// Добавить роль
    /// </summary>
    /// <returns></returns>
    [HttpPost]
    [HasPermission("insert-role")]
    public async Task<AddRoleDto> Post(AddRoleCommand command)
    {
        var dto = await Mediator.Send(command);
        return dto;
    }
    
    /// <summary>
    /// Обновить роль
    /// </summary>
    /// <returns></returns>
    [HttpPut]
    [HasPermission("update-role")]
    public async Task<UpdateRoleDto> Put(UpdateRoleCommand command)
    {
        var dto = await Mediator.Send(command);
        return dto;
    }
    
    /// <summary>
    /// Удалить роль
    /// </summary>
    /// <returns></returns>
    [HttpDelete("{roleId}")]
    [HasPermission("delete-role")]
    public async Task<DeleteRoleDto> Delete(int roleId)
    {
        var dto = await Mediator.Send(new DeleteRoleCommand(roleId));
        return dto;
    }
}