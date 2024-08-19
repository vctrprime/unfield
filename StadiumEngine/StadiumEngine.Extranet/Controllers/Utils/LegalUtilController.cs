using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using StadiumEngine.DTO.Utils;
using StadiumEngine.Commands.Utils;
using StadiumEngine.Extranet.Infrastructure.Attributes;

namespace StadiumEngine.Extranet.Controllers.Utils;

/// <summary>
///     Util-запросы для работы с юр. лицами
/// </summary>
[Route( "utils/legals" )]
[AllowAnonymous]
public class LegalUtilController : BaseApiController
{
    /// <summary>
    ///     Добавление нового юр.лица (с суперюзером и стадионами)
    /// </summary>
    /// <param name="command"></param>
    /// <returns></returns>
    [HttpPost]
    [SecuredUtil]
    public async Task<AddLegalDto> Post( [FromBody] AddLegalCommand command )
    {
        AddLegalDto legal = await Mediator.Send( command );

        return legal;
    }
}