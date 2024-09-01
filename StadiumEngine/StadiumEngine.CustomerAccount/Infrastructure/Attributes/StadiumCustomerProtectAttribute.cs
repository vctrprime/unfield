using Mediator;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Primitives;
using StadiumEngine.DTO.Customers;
using StadiumEngine.Queries.Customers;

namespace StadiumEngine.CustomerAccount.Infrastructure.Attributes;

/// <summary>
/// Проверяет что запросы с фронта идут по тому стадиону, по которому авторизован кастомер
/// </summary>
public class StadiumCustomerProtectAttribute : ActionFilterAttribute
{
    /// <summary>
    /// Проверяет что запросы с фронта идут по тому стадиону, по которому авторизован кастомер
    /// </summary>
    /// <param name="context"></param>
    public override void OnActionExecuting( ActionExecutingContext context )
    {
        StringValues requestHeaders = context.HttpContext.Request.Headers[ "SE-Stadium-Id" ];
        string? value = requestHeaders.FirstOrDefault();
        
        if ( value == "0" || String.IsNullOrEmpty( value ) )
        {
            context.Result = new ObjectResult(
                new { Message = "Unauthorized" } ) { StatusCode = StatusCodes.Status401Unauthorized };
        }
        else
        {
            IMediator mediator = ( IMediator )context.HttpContext.RequestServices.GetService( typeof( IMediator ) );
            if ( mediator != null )
            {
                AuthorizedCustomerDto? customer = mediator.Send( new GetAuthorizedCustomerQuery() ).GetAwaiter().GetResult();

                if ( customer != null && customer.Stadiums.Select( x => x.Id ).Contains( Int32.Parse( value ) ) )
                {
                    return;
                }
            }

            context.Result = new ObjectResult(
                new { Message = "Unauthorized" } ) { StatusCode = StatusCodes.Status401Unauthorized };
        }
    }
}