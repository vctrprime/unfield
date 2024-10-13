using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Unfield.Common.Exceptions;
using Unfield.DTO.Accounts.Users;
using Unfield.Commands.Accounts.Users;
using Unfield.Commands.Admin;
using Unfield.Extranet.Infrastructure.Attributes;
using Unfield.Queries.Accounts.Users;

namespace Unfield.Extranet.Controllers.API.Accounts;

/// <summary>
///     Авторизация
/// </summary>
[Route( "api/accounts" )]
public class AuthorizeController : BaseApiController
{
    /// <summary>
    ///     Получить авторизованного юзера
    /// </summary>
    /// <returns></returns>
    [HttpGet]
    public async Task<AuthorizedUserDto> Get( [FromRoute] GetAuthorizedUserQuery query )
    {
        AuthorizedUserDto user = await Mediator.Send( query );
        return user;
    }

    /// <summary>
    ///     Авторизовать
    /// </summary>
    /// <param name="command"></param>
    /// <returns></returns>
    [HttpPost( "login" )]
    [AllowAnonymous]
    public async Task<AuthorizeUserDto> Login( [FromBody] AuthorizeUserCommand command )
    {
        AuthorizeUserDto user = await Mediator.Send( command );

        return await Authorize( user, "Invalid password or login" );
    }

    /// <summary>
    ///     Сменить стадион
    /// </summary>
    /// <param name="command"></param>
    /// <returns></returns>
    [HttpPut( "change-stadium/{stadiumId}" )]
    public async Task<AuthorizeUserDto> ChangeStadium( [FromRoute] ChangeStadiumCommand command )
    {
        AuthorizeUserDto user = await Mediator.Send( command );

        return await Authorize( user, "Forbidden" );
    }

    /// <summary>
    ///     Сменить группу (админ)
    /// </summary>
    /// <param name="command"></param>
    /// <returns></returns>
    [AdminFeature]
    [HttpPut( "/api/admin/change-stadium-group/{stadiumGroupId}" )]
    public async Task<AuthorizeUserDto> ChangeStadiumGroup( [FromRoute] ChangeStadiumGroupCommand command )
    {
        AuthorizeUserDto user = await Mediator.Send( command );

        return await Authorize( user, "Forbidden" );
    }

    /// <summary>
    ///     Выйти из системы
    /// </summary>
    [HttpDelete( "logout" )]
    [AllowAnonymous]
    public async Task Logout()
    {
        if ( User.Identity is { IsAuthenticated: true } )
        {
            await HttpContext.SignOutAsync();
        }
    }

    private async Task<AuthorizeUserDto> Authorize( AuthorizeUserDto user, string exceptionMessage )
    {
        if ( user == null )
        {
            throw new DomainException( exceptionMessage );
        }

        ClaimsIdentity claimsIdentity = new( user.Claims, "Identity.Core" );

        if ( User.Identity is { IsAuthenticated: true } )
        {
            await HttpContext.SignOutAsync();
        }

        await HttpContext.SignInAsync(
            "Identity.Core",
            new ClaimsPrincipal( claimsIdentity ),
            new AuthenticationProperties { IsPersistent = true } );

        return user;
    }
}