using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Unfield.DTO.Utils;
using Unfield.Commands.Utils;
using Unfield.Extranet.Infrastructure.Attributes;

namespace Unfield.Extranet.Controllers.Utils;

/// <summary>
///     Util-запросы для работы с группами
/// </summary>
[Route( "utils/stadium-groups" )]
[AllowAnonymous]
public class StadiumGroupUtilController : BaseApiController
{
    /// <summary>
    ///     Добавление новой группы (с суперюзером и стадионами)
    /// </summary>
    /// <param name="command"></param>
    /// <returns></returns>
    [HttpPost]
    [SecuredUtil]
    public async Task<AddStadiumGroupDto> Post( [FromBody] AddStadiumGroupCommand command )
    {
        AddStadiumGroupDto stadiumGroup = await Mediator.Send( command );

        return stadiumGroup;
    }
}