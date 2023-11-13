using Mediator;
using Microsoft.AspNetCore.Mvc;

namespace StadiumEngine.Portal.Controllers;

/// <summary>
///     Базовый api-контроллер
/// </summary>
[ApiController]
public class BaseApiController : ControllerBase
{
    private IMediator _mediator;

    /// <summary>
    ///     Объект медиатора
    /// </summary>
    protected IMediator Mediator => _mediator ??= HttpContext.RequestServices.GetService<IMediator>();
}