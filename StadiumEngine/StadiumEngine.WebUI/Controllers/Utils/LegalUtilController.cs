using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using StadiumEngine.DTO.Accounts;
using StadiumEngine.DTO.Utils;
using StadiumEngine.Handlers.Commands.Utils;

namespace StadiumEngine.WebUI.Controllers.Utils;

[Route("utils/legal")]
[AllowAnonymous]
public class LegalUtilController : BaseApiController
{
    //toDo атрибут авторизации по ключу
    [HttpPost]
    public async Task<AddLegalDto> Post(AddLegalCommand command)
    {
        var legal = await Mediator.Send(command);

        return legal;
    }
}