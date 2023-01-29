using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using StadiumEngine.DTO.Accounts;
using StadiumEngine.Handlers.Commands.Accounts;

namespace StadiumEngine.WebUI.Controllers.API.Accounts;

/// <summary>
/// Работа с паролем пользователя
/// </summary>
[Route("api/accounts/user-password")]
public class UserPasswordController : BaseApiController
{
    /// <summary>
    /// Сменить пароль пользователя
    /// </summary>
    /// <returns></returns>
    [HttpPut]
    public async Task<ChangeUserPasswordDto> Put(ChangeUserPasswordCommand command)
    {
        var dto = await Mediator.Send(command);
        return dto;
    }
}