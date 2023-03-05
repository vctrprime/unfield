using Mediator;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using StadiumEngine.Handlers.Queries.Accounts.Users;

namespace StadiumEngine.WebUI.Infrastructure.Attributes;

/// <summary>
///     Атрибут проверки разрешений на админский метод
/// </summary>
public class AdminFeatureAttribute : ActionFilterAttribute
{
    /// <summary>
    ///     Проверить разрешение перед выполнением метода
    /// </summary>
    /// <param name="context"></param>
    public override void OnActionExecuting( ActionExecutingContext context )
    {
        var mediator = ( IMediator )context.HttpContext.RequestServices.GetService( typeof( IMediator ) );
        if (mediator != null)
        {
            var user = mediator.Send( new GetAuthorizedUserQuery() ).GetAwaiter().GetResult();

            if (user.IsAdmin) return;
        }

        context.Result = new ObjectResult(
            new
            {
                Message = "Forbidden"
            } )
        {
            StatusCode = StatusCodes.Status403Forbidden
        };
    }
}