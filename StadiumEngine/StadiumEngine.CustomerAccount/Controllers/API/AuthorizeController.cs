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
[Route( "api/authorize" )]
public class AuthorizeController : BaseApiController
{
    /// <summary>
    ///     Получить авторизованного юзера
    /// </summary>
    /// <returns></returns>
    [HttpGet]
    public async Task<AuthorizedCustomerDto> Get( [FromRoute] GetAuthorizedCustomerQuery query )
    {
        AuthorizedCustomerDto user = await Mediator.Send( query );
        return user;
    }

    /// <summary>
    ///     Авторизовать через редирект
    /// </summary>
    /// <param name="command"></param>
    /// <returns></returns>
    [HttpPost( "redirect" )]
    [AllowAnonymous]
    public async Task<AuthorizeCustomerDto> Redirect( [FromBody] AuthorizeCustomerByRedirectCommand command )
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
            new AuthenticationProperties { IsPersistent = true } );

        return customer;
    }
}