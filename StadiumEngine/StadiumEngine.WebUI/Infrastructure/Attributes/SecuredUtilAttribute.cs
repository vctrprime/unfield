using System;
using System.Linq;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace StadiumEngine.WebUI.Infrastructure.Attributes;

/// <summary>
/// Атрибут для защиты использования utils по апи ключу
/// </summary>
public class SecuredUtilAttribute : ActionFilterAttribute
{
    /// <summary>
    /// Проверить апи ключ
    /// </summary>
    /// <param name="context"></param>
    public override void OnActionExecuting(ActionExecutingContext context)
    {
        //Get header 
        var requestHeaders = context.HttpContext.Request.Headers["SE-Utils-Api-Key"];
        var value = requestHeaders.FirstOrDefault();

        if (string.IsNullOrEmpty(value) || value != Environment.GetEnvironmentVariable("UTILS_API_KEY"))
        {
            context.Result = new ObjectResult(new
            {
                Message = "Доступ запрещен!",
            })
            {
                StatusCode = StatusCodes.Status403Forbidden
            };

        }
    }
}