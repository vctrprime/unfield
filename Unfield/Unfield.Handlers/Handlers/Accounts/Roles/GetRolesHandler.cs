using AutoMapper;
using Unfield.Domain.Entities.Accounts;
using Unfield.Domain.Services.Core.Accounts;
using Unfield.Domain.Services.Identity;
using Unfield.DTO.Accounts.Roles;
using Unfield.Queries.Accounts.Roles;

namespace Unfield.Handlers.Handlers.Accounts.Roles;

internal sealed class GetRolesHandler : BaseRequestHandler<GetRolesQuery, List<RoleDto>>
{
    private readonly IRoleQueryService _queryService;

    public GetRolesHandler(
        IRoleQueryService queryService,
        IMapper mapper,
        IClaimsIdentityService claimsIdentityService ) : base( mapper, claimsIdentityService )
    {
        _queryService = queryService;
    }

    public override async ValueTask<List<RoleDto>> Handle( GetRolesQuery request, CancellationToken cancellationToken )
    {
        List<Role> roles = await _queryService.GetRolesForStadiumGroupAsync( _stadiumGroupId );

        List<RoleDto>? rolesDto = Mapper.Map<List<RoleDto>>( roles );

        return rolesDto;
    }
}