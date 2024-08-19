using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using StadiumEngine.DTO.Utils;
using StadiumEngine.Commands.Utils;
using StadiumEngine.Extranet.Infrastructure.Attributes;

namespace StadiumEngine.Extranet.Controllers.Utils;

/// <summary>
///     Util-запросы для работы с пользователями
/// </summary>
[Route( "utils/users" )]
[AllowAnonymous]
public class UserUtilController : BaseApiController
{
    /// <summary>
    ///     Добавление нового админа
    /// </summary>
    /// <param name="command"></param>
    /// <returns></returns>
    [HttpPost( "new-admin" )]
    [SecuredUtil]
    public async Task<AddAdminUserDto> Post( [FromBody] AddAdminUserCommand command )
    {
        AddAdminUserDto dto = await Mediator.Send( command );

        return dto;
    }
}