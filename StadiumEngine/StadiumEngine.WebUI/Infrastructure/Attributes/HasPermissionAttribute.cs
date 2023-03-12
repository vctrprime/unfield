using System.Collections.Generic;
using System.Linq;
using Mediator;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using StadiumEngine.DTO.Accounts.Users;
using StadiumEngine.Queries.Accounts.Users;

namespace StadiumEngine.WebUI.Infrastructure.Attributes;

/// <summary>
///     Атрибут проверки разрешений на метод
/// </summary>
public class HasPermissionAttribute : ActionFilterAttribute
{
    private readonly string _needAlternativePermissions;

    /// <summary>
    ///     Атрибут проверки разрешений на метод
    /// </summary>
    /// <param name="needAlternativePermissions">
    ///     Список разрешений через "," (разрешает выполнение метода, если есть хотя бы
    ///     одно разрешение из поданного списка)
    /// </param>
    public HasPermissionAttribute( string needAlternativePermissions )
    {
        _needAlternativePermissions = needAlternativePermissions.ToLower();
    }

    /// <summary>
    ///     Проверить разрешение перед выполнением метода
    /// </summary>
    /// <param name="context"></param>
    public override void OnActionExecuting( ActionExecutingContext context )
    {
        IMediator mediator = ( IMediator )context.HttpContext.RequestServices.GetService( typeof( IMediator ) );
        if ( mediator != null )
        {
            List<UserPermissionDto> permissions =
                mediator.Send( new GetUserPermissionsQuery() ).GetAwaiter().GetResult();

            string[] needAlternativePermissions = _needAlternativePermissions.Split( "," );

            if ( permissions.Any(
                    permission => needAlternativePermissions.Contains( permission.Name.ToLower() ) ) )
            {
                return;
            }
        }

        context.Result = new ObjectResult(
            new { Message = "Fordidden" } ) { StatusCode = StatusCodes.Status403Forbidden };
    }
}