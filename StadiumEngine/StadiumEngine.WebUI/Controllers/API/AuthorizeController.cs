using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using StadiumEngine.Common.Exceptions;
using StadiumEngine.DTO.Auth;
using Swashbuckle.AspNetCore.Annotations;

namespace StadiumEngine.WebUI.Controllers.API;

/// <summary>
/// Авторизация
/// </summary>
[Route("api/account")]
[ApiController]
public class AuthorizeController : ControllerBase
{
    /// <summary>
    /// Авторизовать
    /// </summary>
    /// <param name="dto"></param>
    /// <returns></returns>
    [HttpPost("login")]
    [SwaggerResponse(StatusCodes.Status200OK,
        Type = typeof(AuthUserDto))]
    public async Task<AuthUserDto> Login([FromBody] LoginPostDto dto)
    {
        if (User.Identity.IsAuthenticated)
        {
            await HttpContext.SignOutAsync();
        }
        
        if (dto.Username == "admin" && dto.Password == "123456")
        {
            var claimsIdentity = new ClaimsIdentity(System.Array.Empty<Claim>(), "Identity.Application");

            // Аутентификация.
            await HttpContext.SignInAsync(
                "Identity.Application",
                new ClaimsPrincipal(claimsIdentity),
                new AuthenticationProperties
                {
                    IsPersistent = true
                });
            
            return new AuthUserDto
            {
                Username = dto.Username
            };
        }
        throw new DomainException("invalid password or username");
    }

    /// <summary>
    /// Выйти из системы
    /// </summary>
    [HttpDelete("logout")]
    [SwaggerResponse(StatusCodes.Status200OK)]
    public async Task Logout()
    {
        if (User.Identity.IsAuthenticated)
        {
            await HttpContext.SignOutAsync();
        }
    }


}