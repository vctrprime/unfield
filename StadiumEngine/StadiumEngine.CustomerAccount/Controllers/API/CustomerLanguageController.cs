using Microsoft.AspNetCore.Mvc;
using StadiumEngine.Commands.Customers;
using StadiumEngine.DTO.Customers;

namespace StadiumEngine.CustomerAccount.Controllers.API;

/// <summary>
///     Язык заказчика
/// </summary>
[Route( "api/accounts/customer-language" )]
public class CustomerLanguageController : BaseApiController
{
    /// <summary>
    ///     Сменить язык заказчика
    /// </summary>
    /// <returns></returns>
    [HttpPut( "{language}" )]
    public async Task<ChangeCustomerLanguageDto> Put( [FromRoute] ChangeCustomerLanguageCommand command )
    {
        ChangeCustomerLanguageDto dto = await Mediator.Send( command );
        return dto;
    }
}