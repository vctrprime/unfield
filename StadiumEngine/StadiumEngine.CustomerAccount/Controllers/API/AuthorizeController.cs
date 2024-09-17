using System.Security.Claims;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using StadiumEngine.Commands.Customers;
using StadiumEngine.Common.Exceptions;
using StadiumEngine.DTO.Customers;
using StadiumEngine.Queries.Customers;

namespace StadiumEngine.CustomerAccount.Controllers.API;

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
    [HttpGet( "authorize" )]
    public async Task<AuthorizedCustomerDto?> Get( [FromRoute] GetAuthorizedCustomerQuery query )
    {
        AuthorizedCustomerDto? customer = await Mediator.Send( query );
        return customer;
    }

    /// <summary>
    ///     Авторизовать через редирект
    /// </summary>
    /// <param name="command"></param>
    /// <returns></returns>
    [HttpPost( "login/redirect" )]
    [AllowAnonymous]
    public async Task<AuthorizeCustomerDto> Redirect( [FromBody] AuthorizeCustomerByRedirectCommand command )
    {
        AuthorizeCustomerDto customer = await Mediator.Send( command );

        return await Authorize( customer, "System error" );
    }
    
    /// <summary>
    ///     Авторизовать через логин и пароль
    /// </summary>
    /// <param name="command"></param>
    /// <returns></returns>
    [HttpPost( "login" )]
    [AllowAnonymous]
    public async Task<AuthorizeCustomerDto> Login( AuthorizeCustomerCommand command )
    {
        AuthorizeCustomerDto customer = await Mediator.Send( command );

        return await Authorize( customer, "System error" );
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

    private async Task<AuthorizeCustomerDto> Authorize( AuthorizeCustomerDto customer, string exceptionMessage )
    {
        if ( customer == null )
        {
            throw new DomainException( exceptionMessage );
        }

        ClaimsIdentity claimsIdentity = new( customer.Claims, "Identity.Core" );

        if ( User.Identity is { IsAuthenticated: true } )
        {
            await HttpContext.SignOutAsync();
        }

        await HttpContext.SignInAsync(
            "Identity.Core",
            new ClaimsPrincipal( claimsIdentity ),
            new AuthenticationProperties
            {
                IsPersistent = true
            } );

        return customer;
    }
}