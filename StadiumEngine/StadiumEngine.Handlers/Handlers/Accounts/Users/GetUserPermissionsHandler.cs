using AutoMapper;
using StadiumEngine.Domain.Services.Facades.Accounts;
using StadiumEngine.Domain.Services.Identity;
using StadiumEngine.DTO.Accounts.Users;
using StadiumEngine.Handlers.Queries.Accounts.Users;

namespace StadiumEngine.Handlers.Handlers.Accounts.Users;

internal sealed class GetUserPermissionsHandler : BaseRequestHandler<GetUserPermissionsQuery, List<UserPermissionDto>>
{
    private readonly IUserQueryFacade _userFacade;

    public GetUserPermissionsHandler(
        IUserQueryFacade userFacade,
        IMapper mapper,
        IClaimsIdentityService claimsIdentityService ) : base( mapper, claimsIdentityService )
    {
        _userFacade = userFacade;
    }

    public override async ValueTask<List<UserPermissionDto>> Handle( GetUserPermissionsQuery request,
        CancellationToken cancellationToken )
    {
        var permissions = await _userFacade.GetUserPermissions( _userId );

        var permissionsDto = Mapper.Map<List<UserPermissionDto>>( permissions );

        return permissionsDto;
    }
}