using AutoMapper;
using StadiumEngine.Domain;
using StadiumEngine.Domain.Services;
using StadiumEngine.Domain.Services.Facades.Accounts;
using StadiumEngine.Domain.Services.Identity;
using StadiumEngine.DTO.Accounts.Roles;
using StadiumEngine.Handlers.Queries.Accounts.Roles;

namespace StadiumEngine.Handlers.Handlers.Accounts.Roles;

internal sealed class GetRolesHandler : BaseRequestHandler<GetRolesQuery, List<RoleDto>>
{
    private readonly IRoleFacade _roleFacade;

    public GetRolesHandler(
        IRoleFacade roleFacade,
        IMapper mapper, 
        IClaimsIdentityService claimsIdentityService) : base(mapper, claimsIdentityService)
    {
        _roleFacade = roleFacade;
    }
    
    public override async ValueTask<List<RoleDto>> Handle(GetRolesQuery request, CancellationToken cancellationToken)
    {
        var roles = await _roleFacade.GetRolesForLegal(_legalId);

        var rolesDto = Mapper.Map<List<RoleDto>>(roles);

        return rolesDto;
    }
}