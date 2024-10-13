using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Unfield.Commands.Customers;
using Unfield.DTO.Customers;

namespace Unfield.CustomerAccount.Controllers.API;

/// <summary>
///     Работа с паролем заказчика
/// </summary>
[Route( "api/accounts/customer-password" )]
public class CustomerPasswordController : BaseApiController
{
    /// <summary>
    ///     Сменить пароль заказчика
    /// </summary>
    /// <returns></returns>
    [HttpPut( "change" )]
    public async Task<ChangeCustomerPasswordDto> Change( ChangeCustomerPasswordCommand command )
    {
        ChangeCustomerPasswordDto dto = await Mediator.Send( command );
        return dto;
    }

    /// <summary>
    ///     Сбросить пароль заказчика
    /// </summary>
    /// <returns></returns>
    [AllowAnonymous]
    [HttpPut( "reset" )]
    public async Task<ResetCustomerPasswordDto> Reset( ResetCustomerPasswordCommand command )
    {
        ResetCustomerPasswordDto dto = await Mediator.Send( command );
        return dto;
    }
}