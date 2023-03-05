using AutoMapper;
using StadiumEngine.Domain;
using StadiumEngine.Domain.Services;
using StadiumEngine.Domain.Services.Facades.Accounts;
using StadiumEngine.Domain.Services.Identity;
using StadiumEngine.DTO.Accounts.Permissions;
using StadiumEngine.Handlers.Queries.Accounts.Roles;

namespace StadiumEngine.Handlers.Handlers.Accounts.Roles;

internal sealed class GetPermissionsForRoleHandler : BaseRequestHandler<GetPermissionsForRoleQuery, List<PermissionDto>>
{
    private readonly IRoleQueryFacade _roleFacade;

    public GetPermissionsForRoleHandler(
        IRoleQueryFacade roleFacade,
        IMapper mapper,
        IClaimsIdentityService claimsIdentityService ) : base( mapper, claimsIdentityService )
    {
        _roleFacade = roleFacade;
    }

    public override async ValueTask<List<PermissionDto>> Handle( GetPermissionsForRoleQuery request,
        CancellationToken cancellationToken )
    {
        var permissions = await _roleFacade.GetPermissionsForRole( request.RoleId, _legalId );

        var permissionsDto = Mapper.Map<List<PermissionDto>>( permissions.Keys );

        permissionsDto.ForEach(
            pd => { pd.IsRoleBound = permissions.FirstOrDefault( p => p.Key.Id == pd.Id ).Value; } );

        return permissionsDto;
    }
}