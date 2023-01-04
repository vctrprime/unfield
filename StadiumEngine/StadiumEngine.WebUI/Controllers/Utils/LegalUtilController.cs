using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using StadiumEngine.DTO.Utils;
using StadiumEngine.Handlers.Commands.Utils;

namespace StadiumEngine.WebUI.Controllers.Utils;

/// <summary>
/// Util-запросы для работы с юр. лицами
/// </summary>
[Route("utils/legal")]
[AllowAnonymous]
public class LegalUtilController : BaseApiController
{
    //toDo атрибут авторизации по ключу
    /// <summary>
    /// Добавление нового юр.лица (с суперюзером и стадионами)
    /// </summary>
    /// <param name="command"></param>
    /// <returns></returns>
    [HttpPost]
    public async Task<AddLegalDto> Post(AddLegalCommand command)
    {
        var legal = await Mediator.Send(command);

        return legal;
    }
}