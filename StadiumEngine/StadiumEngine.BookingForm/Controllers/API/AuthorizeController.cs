using Microsoft.AspNetCore.Mvc;
using StadiumEngine.DTO.Customers;
using StadiumEngine.Queries.Customers;

namespace StadiumEngine.BookingForm.Controllers.API;

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
}