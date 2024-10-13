using Mediator;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Primitives;
using Unfield.DTO.Customers;
using Unfield.Queries.Customers;

namespace Unfield.CustomerAccount.Infrastructure.Attributes;

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
        StringValues requestHeaders = context.HttpContext.Request.Headers[ "SE-Stadium-Token" ];
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

                if ( customer?.Stadiums != null && customer.Stadiums.Select( x => x.Token ).Contains( value  ) )
                {
                    return;
                }
            }

            context.Result = new ObjectResult(
                new { Message = "Unauthorized" } ) { StatusCode = StatusCodes.Status401Unauthorized };
        }
    }
}