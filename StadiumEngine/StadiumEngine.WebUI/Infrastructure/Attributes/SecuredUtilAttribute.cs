#nullable enable
using System;
using System.Linq;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Primitives;
using StadiumEngine.Common.Configuration;

namespace StadiumEngine.WebUI.Infrastructure.Attributes;

/// <summary>
///     Атрибут для защиты использования utils по апи ключу
/// </summary>
public class SecuredUtilAttribute : ActionFilterAttribute
{
    /// <summary>
    ///     Проверить апи ключ
    /// </summary>
    /// <param name="context"></param>
    public override void OnActionExecuting( ActionExecutingContext context )
    {
        //Get header 
        StringValues requestHeaders = context.HttpContext.Request.Headers[ "SE-Utils-Api-Key" ];
        string? value = requestHeaders.FirstOrDefault();

        UtilsConfig? config = ( UtilsConfig? )context.HttpContext.RequestServices.GetService( typeof( UtilsConfig ) );

        if ( String.IsNullOrEmpty( value ) || config == null || value != config.UtilsApiKey )
        {
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
}