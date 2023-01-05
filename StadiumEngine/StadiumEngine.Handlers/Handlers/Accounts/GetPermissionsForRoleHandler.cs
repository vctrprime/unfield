using AutoMapper;
using StadiumEngine.Common.Exceptions;
using StadiumEngine.Domain.Repositories.Accounts;
using StadiumEngine.Domain.Services;
using StadiumEngine.Domain.Services.Identity;
using StadiumEngine.DTO.Accounts;
using StadiumEngine.Handlers.Queries.Accounts;

namespace StadiumEngine.Handlers.Handlers.Accounts;

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
        var legalId = ClaimsIdentityService.GetLegalId();
        var role = await _roleRepository.Get(request.RoleId);

        if (role == null || legalId != role.LegalId) throw new DomainException("Указанная роль не найдена!");

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