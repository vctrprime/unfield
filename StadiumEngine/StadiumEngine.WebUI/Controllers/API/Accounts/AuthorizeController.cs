using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using StadiumEngine.Common.Exceptions;
using StadiumEngine.DTO.Accounts;
using StadiumEngine.Handlers.Commands.Accounts;
using StadiumEngine.Handlers.Queries.Accounts;

namespace StadiumEngine.WebUI.Controllers.API.Accounts;

/// <summary>
/// Авторизация
/// </summary>
[Route("api/account")]
public class AuthorizeController : BaseApiController
{
    /// <summary>
    /// Получить авторизованного юзера
    /// </summary>
    /// <returns></returns>
    [HttpGet]
    public async Task<AuthorizeUserDto> Get()
    {
        var user = await Mediator.Send(new GetAuthorizedUserQuery());
        return user;
    }
    
    /// <summary>
    /// Авторизовать
    /// </summary>
    /// <param name="command"></param>
    /// <returns></returns>
    [HttpPost("login")]
    [AllowAnonymous]
    public async Task<AuthorizeUserDto> Login([FromBody] AuthorizeUserCommand command)
    {
        var user = await Mediator.Send(command);

        return await Authorize(user, "Invalid password or login");
    }
    
    /// <summary>
    /// Сменить стадион
    /// </summary>
    /// <param name="stadiumId"></param>
    /// <returns></returns>
    [HttpPut("change-stadium/{stadiumId}")]
    public async Task<AuthorizeUserDto> ChangeStadium(int stadiumId)
    {
        var user = await Mediator.Send(new ChangeStadiumCommand
        {
            StadiumId = stadiumId
        });

        return await Authorize(user, "Forbidden");
    }

    /// <summary>
    /// Выйти из системы
    /// </summary>
    [HttpDelete("logout")]
    [AllowAnonymous]
    public async Task Logout()
    {
        if (User.Identity is { IsAuthenticated: true })
        {
            await HttpContext.SignOutAsync();
        }
    }

    private async Task<AuthorizeUserDto> Authorize(AuthorizeUserDto user, string exceptionMessage)
    {
        if (user == null) throw new DomainException(exceptionMessage);
        
        var claimsIdentity = new ClaimsIdentity(user.Claims, "Identity.Application");
        
        if (User.Identity is { IsAuthenticated: true })
        {
            await HttpContext.SignOutAsync();
        }
        
        await HttpContext.SignInAsync(
            "Identity.Application",
            new ClaimsPrincipal(claimsIdentity),
            new AuthenticationProperties
            {
                IsPersistent = true
            });

        return user;
    }

}