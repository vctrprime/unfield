using System.Linq;
using System.Threading.Tasks;
using Mediator;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using StadiumEngine.Handlers.Queries.Accounts;

namespace StadiumEngine.WebUI.Infrastructure.Attributes;

/// <summary>
/// Атрибут проверки разрешений на метод
/// </summary>
public class HasPermissionAttribute : ActionFilterAttribute
{
    private readonly string _permission;
    
    /// <summary>
    /// Атрибут проверки разрешений на метод
    /// </summary>
    public HasPermissionAttribute(string permission)
    {
        _permission = permission;
    }
    
    /// <summary>
    /// Проверить разрешение перед выполнением метода
    /// </summary>
    /// <param name="context"></param>
    public override void OnActionExecuting(ActionExecutingContext context)
    {
        var mediator = (IMediator)context.HttpContext.RequestServices.GetService(typeof(IMediator));
        if (mediator != null)
        {
            var permissions = mediator.Send(new GetUserPermissionsQuery()).GetAwaiter().GetResult();
            
            if (permissions.Select(p => p.Name.ToLower()).Contains(_permission.ToLower())) return;
        }
        
        context.Result = new ObjectResult(new
        {
            Message = "Доступ запрещен!",
        })
        {
            StatusCode = StatusCodes.Status403Forbidden
        };
       
    }
}