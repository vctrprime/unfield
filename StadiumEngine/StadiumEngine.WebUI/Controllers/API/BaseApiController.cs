using Mediator;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;

namespace StadiumEngine.WebUI.Controllers.API;

[ApiController]
[Authorize]
public class BaseApiController : ControllerBase
{
   private IMediator _mediator;

   protected IMediator Mediator => _mediator ??= HttpContext.RequestServices.GetService<IMediator>();
   
}