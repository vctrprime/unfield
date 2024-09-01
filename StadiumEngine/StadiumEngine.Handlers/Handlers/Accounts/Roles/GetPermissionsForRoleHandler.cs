using AutoMapper;
using StadiumEngine.Domain.Entities.Accounts;
using StadiumEngine.Domain.Services.Core.Accounts;
using StadiumEngine.Domain.Services.Identity;
using StadiumEngine.DTO.Accounts.Permissions;
using StadiumEngine.Queries.Accounts.Roles;

namespace StadiumEngine.Handlers.Handlers.Accounts.Roles;

internal sealed class GetPermissionsForRoleHandler : BaseRequestHandler<GetPermissionsForRoleQuery, List<PermissionDto>>
{
    private readonly IRoleQueryService _queryService;

    public GetPermissionsForRoleHandler(
        IRoleQueryService queryService,
        IMapper mapper,
        IClaimsIdentityService claimsIdentityService ) : base( mapper, claimsIdentityService )
    {
        _queryService = queryService;
    }

    public override async ValueTask<List<PermissionDto>> Handle( GetPermissionsForRoleQuery request,
        CancellationToken cancellationToken )
    {
        Dictionary<Permission, bool> permissions = await _queryService.GetPermissionsForRoleAsync( request.RoleId, _stadiumGroupId );

        List<PermissionDto>? permissionsDto = Mapper.Map<List<PermissionDto>>( permissions.Keys );

        permissionsDto.ForEach(
            pd => { pd.IsRoleBound = permissions.FirstOrDefault( p => p.Key.Id == pd.Id ).Value; } );

        return permissionsDto;
    }
}