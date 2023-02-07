using AutoMapper;
using StadiumEngine.Domain.Repositories.Accounts;
using StadiumEngine.Domain.Services;
using StadiumEngine.Domain.Services.Identity;
using StadiumEngine.DTO.Accounts;
using StadiumEngine.DTO.Accounts.Permissions;
using StadiumEngine.Handlers.Queries.Accounts;
using StadiumEngine.Handlers.Queries.Accounts.Roles;

namespace StadiumEngine.Handlers.Handlers.Accounts.Roles;

internal sealed class GetPermissionsForRoleHandler : BaseRequestHandler<GetPermissionsForRoleQuery, List<PermissionDto>>
{
    private readonly IPermissionRepository _permissionRepository;
    private readonly IRoleRepository _roleRepository;

    public GetPermissionsForRoleHandler(IMapper mapper, IClaimsIdentityService claimsIdentityService, IUnitOfWork unitOfWork, 
        IPermissionRepository permissionRepository, IRoleRepository roleRepository) : base(mapper, claimsIdentityService, unitOfWork)
    {
        _permissionRepository = permissionRepository;
        _roleRepository = roleRepository;
    }
    public override async ValueTask<List<PermissionDto>> Handle(GetPermissionsForRoleQuery request, CancellationToken cancellationToken)
    {
        var role = await _roleRepository.Get(request.RoleId);
        CheckRoleAccess(role);

        var permissions = await _permissionRepository.GetAll();
        var rolePermissions = await _permissionRepository.GetForRole(request.RoleId);

        var permissionsDto = Mapper.Map<List<PermissionDto>>(permissions);
        
        permissionsDto.ForEach(p =>
        {
            p.IsRoleBound = rolePermissions.FirstOrDefault(rp => rp.Id == p.Id) != null;
        });

        return permissionsDto;
    }
}