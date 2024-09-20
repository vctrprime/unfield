using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using StadiumEngine.DTO.Accounts.Stadiums;
using StadiumEngine.Queries.Customers;

namespace StadiumEngine.CustomerAccount.Controllers.API;

/// <summary>
/// Работа со стадионом
/// </summary>
[Route( "api/accounts/stadium" )]
public class StadiumController : BaseApiController
{
    /// <summary>
    /// Получить инфо по стадиону
    /// </summary>
    /// <param name="query"></param>
    /// <returns></returns>
    [HttpGet]
    [AllowAnonymous]
    public async Task<StadiumDto> Get( [FromRoute] GetStadiumQuery query )
    {
        StadiumDto stadium = await Mediator.Send( query );
        return stadium;
    }
}