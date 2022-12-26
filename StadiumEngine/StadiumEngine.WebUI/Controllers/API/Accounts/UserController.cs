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
[Route("api/users")]
public class UserController : BaseApiController
{
    /// <summary>
    /// Пользователи
    /// </summary>
    /// <returns></returns>
    [HttpGet]
    [HasPermission("get-users")]
    public async Task<List<UserStadiumDto>> Get()
    {
        var stadiums = await Mediator.Send(new GetUserStadiumsQuery());
        return stadiums;
    }
}