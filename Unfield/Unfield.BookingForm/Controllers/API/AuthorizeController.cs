using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Unfield.DTO.Customers;
using Unfield.Queries.Customers;

namespace Unfield.BookingForm.Controllers.API;

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
    public async Task<AuthorizedCustomerDto?> Get( [FromRoute] GetAuthorizedCustomerQuery query )
    {
        AuthorizedCustomerDto? user = await Mediator.Send( query );
        return user;
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
}