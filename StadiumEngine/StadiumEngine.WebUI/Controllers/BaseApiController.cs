using Mediator;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;

namespace StadiumEngine.WebUI.Controllers;

/// <summary>
/// Базовый api-контроллер
/// </summary>
[ApiController]
[Authorize]
public class BaseApiController : ControllerBase
{
    private IMediator _mediator;

    /// <summary>
    /// Объект медиатора
    /// </summary>
    protected IMediator Mediator => _mediator ??= HttpContext.RequestServices.GetService<IMediator>();
}