using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using StadiumEngine.Commands.Customers;
using StadiumEngine.DTO.Customers;

namespace StadiumEngine.CustomerAccount.Controllers.API;

/// <summary>
/// Регистрация
/// </summary>
[Route( "api/accounts" )]
public class RegisterController : BaseApiController
{
    /// <summary>
    ///     Регистрация
    /// </summary>
    /// <param name="command"></param>
    /// <returns></returns>
    [HttpPost( "register" )]
    [AllowAnonymous]
    public async Task<RegisterCustomerDto> Redirect( RegisterCustomerCommand command )
    {
        RegisterCustomerDto result = await Mediator.Send( command );

        return result;
    }
}