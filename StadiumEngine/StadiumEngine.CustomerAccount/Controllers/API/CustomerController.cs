using Microsoft.AspNetCore.Mvc;
using StadiumEngine.Commands.Customers;
using StadiumEngine.DTO.Customers;

namespace StadiumEngine.CustomerAccount.Controllers.API;

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
    public async Task<UpdateCustomerDto> Change( UpdateCustomerCommand command )
    {
        UpdateCustomerDto dto = await Mediator.Send( command );
        return dto;
    }
}