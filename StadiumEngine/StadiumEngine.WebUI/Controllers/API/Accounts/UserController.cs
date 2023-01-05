using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using StadiumEngine.DTO.Accounts;
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
}