using AutoMapper;
using StadiumEngine.Domain.Entities.Accounts;
using StadiumEngine.Domain.Services.Facades.Accounts;
using StadiumEngine.Domain.Services.Identity;
using StadiumEngine.DTO.Accounts.Roles;
using StadiumEngine.Handlers.Queries.Accounts.Roles;

namespace StadiumEngine.Handlers.Handlers.Accounts.Roles;

internal sealed class GetRolesHandler : BaseRequestHandler<GetRolesQuery, List<RoleDto>>
{
    private readonly IRoleQueryFacade _roleFacade;

    public GetRolesHandler(
        IRoleQueryFacade roleFacade,
        IMapper mapper,
        IClaimsIdentityService claimsIdentityService ) : base( mapper, claimsIdentityService )
    {
        _roleFacade = roleFacade;
    }

    public override async ValueTask<List<RoleDto>> Handle( GetRolesQuery request, CancellationToken cancellationToken )
    {
        List<Role> roles = await _roleFacade.GetRolesForLegal( _legalId );

        List<RoleDto>? rolesDto = Mapper.Map<List<RoleDto>>( roles );

        return rolesDto;
    }
}