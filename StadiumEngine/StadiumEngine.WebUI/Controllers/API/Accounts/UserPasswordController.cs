using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using StadiumEngine.DTO.Accounts;
using StadiumEngine.DTO.Accounts.Users;
using StadiumEngine.Handlers.Commands.Accounts;
using StadiumEngine.Handlers.Commands.Accounts.Users;

namespace StadiumEngine.WebUI.Controllers.API.Accounts;

/// <summary>
/// Работа с паролем пользователя
/// </summary>
[Route( "api/accounts/user-password" )]
public class UserPasswordController : BaseApiController
{
    /// <summary>
    /// Сменить пароль пользователя
    /// </summary>
    /// <returns></returns>
    [HttpPut( "change" )]
    public async Task<ChangeUserPasswordDto> Change( ChangeUserPasswordCommand command )
    {
        var dto = await Mediator.Send( command );
        return dto;
    }

    /// <summary>
    /// Сбросить пароль пользователя
    /// </summary>
    /// <returns></returns>
    [AllowAnonymous]
    [HttpPut( "reset" )]
    public async Task<ResetUserPasswordDto> Reset( ResetUserPasswordCommand command )
    {
        var dto = await Mediator.Send( command );
        return dto;
    }
}