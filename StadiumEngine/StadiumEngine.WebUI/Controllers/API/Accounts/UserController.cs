using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using StadiumEngine.DTO.Accounts;
using StadiumEngine.Handlers.Commands.Accounts;
using StadiumEngine.Handlers.Queries.Accounts;
using StadiumEngine.WebUI.Infrastructure.Attributes;

namespace StadiumEngine.WebUI.Controllers.API.Accounts;

/// <summary>
/// Пользователи
/// </summary>
[Route("api/accounts/users")]
public class UserController : BaseApiController
{
    /// <summary>
    /// Получить пользователей
    /// </summary>
    /// <returns></returns>
    [HttpGet]
    [HasPermission("get-users")]
    public async Task<List<UserDto>> Get()
    {
        var users = await Mediator.Send(new GetUsersQuery());
        return users;
    }
    
    /// <summary>
    /// Добавить пользователя
    /// </summary>
    /// <param name="command"></param>
    /// <returns></returns>
    [HttpPost]
    [HasPermission("insert-user")]
    public async Task<AddUserDto> Post(AddUserCommand command)
    {
        var dto = await Mediator.Send(command);
        return dto;
    }
    
    /// <summary>
    /// Изменить пользователя
    /// </summary>
    /// <param name="command"></param>
    /// <returns></returns>
    [HttpPut]
    [HasPermission("update-user")]
    public async Task<UpdateUserDto> Put(UpdateUserCommand command)
    {
        var dto = await Mediator.Send(command);
        return dto;
    }
    
    /// <summary>
    /// Удалить пользователя
    /// </summary>
    /// <param name="userId"></param>
    /// <returns></returns>
    [HttpDelete("{userId}")]
    [HasPermission("delete-user")]
    public async Task<DeleteUserDto> Delete(int userId)
    {
        var dto = await Mediator.Send(new DeleteUserCommand(userId));
        return dto;
    }
}