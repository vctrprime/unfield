using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using StadiumEngine.DTO.Accounts.Users;
using StadiumEngine.Commands.Accounts.Users;

namespace StadiumEngine.Extranet.Controllers.API.Accounts;

/// <summary>
///     Язык пользователя
/// </summary>
[Route( "api/accounts/user-language" )]
public class UserLanguageController : BaseApiController
{
    /// <summary>
    ///     Сменить язык пользователя
    /// </summary>
    /// <returns></returns>
    [HttpPut( "{language}" )]
    public async Task<ChangeUserLanguageDto> Put( [FromRoute] ChangeUserLanguageCommand command )
    {
        ChangeUserLanguageDto dto = await Mediator.Send( command );
        return dto;
    }
}