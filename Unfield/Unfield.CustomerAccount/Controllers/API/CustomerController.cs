using Microsoft.AspNetCore.Mvc;
using Unfield.Commands.Customers;
using Unfield.DTO.Customers;

namespace Unfield.CustomerAccount.Controllers.API;

/// <summary>
///     Работа с профилем заказчика
/// </summary>
[Route( "api/accounts/customer" )]
public class CustomerController : BaseApiController
{
    /// <summary>
    ///     Обновить профиль заказчика
    /// </summary>
    /// <returns></returns>
    [HttpPut]
    public async Task<AuthorizedCustomerDto> Update( UpdateCustomerCommand command )
    {
        AuthorizedCustomerDto dto = await Mediator.Send( command );
        return dto;
    }
}