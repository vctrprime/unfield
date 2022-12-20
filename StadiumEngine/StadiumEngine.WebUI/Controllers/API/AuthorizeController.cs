using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using StadiumEngine.Common.Exceptions;
using StadiumEngine.DTO.Accounts;
using StadiumEngine.Handlers.Commands.Accounts;
using Swashbuckle.AspNetCore.Annotations;

namespace StadiumEngine.WebUI.Controllers.API;

/// <summary>
/// Авторизация
/// </summary>
[Route("api/account")]
[ApiController]
public class AuthorizeController : BaseApiController
{
    /// <summary>
    /// Авторизовать
    /// </summary>
    /// <param name="command"></param>
    /// <returns></returns>
    [HttpPost("login")]
    public async Task<AuthorizeUserDto> Login([FromBody] AuthorizeUserCommand command)
    {
        if (User.Identity is { IsAuthenticated: true })
        {
            await HttpContext.SignOutAsync();
        }

        var user = await Mediator.Send(command);
        
        if (user == null) throw new DomainException("Invalid password or login");
        
        var claimsIdentity = new ClaimsIdentity(user.Claims, "Identity.Application");
        
        await HttpContext.SignInAsync(
            "Identity.Application",
            new ClaimsPrincipal(claimsIdentity),
            new AuthenticationProperties
            {
                IsPersistent = true
            });

        return user;
    }

    /// <summary>
    /// Выйти из системы
    /// </summary>
    [HttpDelete("logout")]
    [SwaggerResponse(StatusCodes.Status200OK)]
    public async Task Logout()
    {
        if (User.Identity is { IsAuthenticated: true })
        {
            await HttpContext.SignOutAsync();
        }
    }


}