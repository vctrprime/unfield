using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using StadiumEngine.DTO.Utils;
using StadiumEngine.Handlers.Commands.Utils;
using StadiumEngine.WebUI.Infrastructure.Attributes;

namespace StadiumEngine.WebUI.Controllers.Utils;

/// <summary>
/// Util-запросы для работы с юр. лицами
/// </summary>
[Route( "utils/legals" )]
[AllowAnonymous]
public class LegalUtilController : BaseApiController
{
    /// <summary>
    /// Добавление нового юр.лица (с суперюзером и стадионами)
    /// </summary>
    /// <param name="command"></param>
    /// <returns></returns>
    [HttpPost]
    [SecuredUtil]
    public async Task<AddLegalDto> Post( AddLegalCommand command )
    {
        var legal = await Mediator.Send( command );

        return legal;
    }
}