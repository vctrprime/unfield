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
    private readonly IRoleFacade _roleFacade;

    public GetPermissionsForRoleHandler(
        IRoleFacade roleFacade,
        IMapper mapper, 
        IClaimsIdentityService claimsIdentityService, 
        IUnitOfWork unitOfWork) : base(mapper, claimsIdentityService, unitOfWork)
    {
        _roleFacade = roleFacade;
    }
    public override async ValueTask<List<PermissionDto>> Handle(GetPermissionsForRoleQuery request, CancellationToken cancellationToken)
    {
        var permissions = await _roleFacade.GetPermissionsForRole(request.RoleId, _legalId);

        var permissionsDto = Mapper.Map<List<PermissionDto>>(permissions.Keys);
        
        permissionsDto.ForEach(pd =>
        {
            pd.IsRoleBound = permissions.FirstOrDefault(p => p.Key.Id == pd.Id).Value;
        });

        return permissionsDto;
    }
}