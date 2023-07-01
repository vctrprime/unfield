using AutoMapper;
using StadiumEngine.Domain.Entities.Accounts;
using StadiumEngine.Domain.Services.Core.Accounts;
using StadiumEngine.Domain.Services.Identity;
using StadiumEngine.DTO.Accounts.Roles;
using StadiumEngine.Queries.Accounts.Roles;

namespace StadiumEngine.Handlers.Handlers.Accounts.Roles;

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
        List<Role> roles = await _queryService.GetRolesForLegalAsync( _legalId );

        List<RoleDto>? rolesDto = Mapper.Map<List<RoleDto>>( roles );

        return rolesDto;
    }
}