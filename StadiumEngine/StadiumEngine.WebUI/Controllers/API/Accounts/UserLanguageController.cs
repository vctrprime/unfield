using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using StadiumEngine.DTO.Accounts;
using StadiumEngine.DTO.Accounts.Users;
using StadiumEngine.Handlers.Commands.Accounts;
using StadiumEngine.Handlers.Commands.Accounts.Users;

namespace StadiumEngine.WebUI.Controllers.API.Accounts;

/// <summary>
/// Язык пользователя
/// </summary>
[Route( "api/accounts/user-language" )]
public class UserLanguageController : BaseApiController
{
    /// <summary>
    /// Сменить язык пользователя
    /// </summary>
    /// <returns></returns>
    [HttpPut( "{language}" )]
    public async Task<ChangeUserLanguageDto> Put( string language )
    {
        var dto = await Mediator.Send( new ChangeUserLanguageCommand( language ) );
        return dto;
    }
}