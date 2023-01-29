using AutoMapper;
using StadiumEngine.Common;
using StadiumEngine.Common.Exceptions;
using StadiumEngine.Domain.Entities.Accounts;
using StadiumEngine.Domain.Repositories.Accounts;
using StadiumEngine.Domain.Services;
using StadiumEngine.Domain.Services.Identity;
using StadiumEngine.DTO.Accounts;
using StadiumEngine.Handlers.Commands.Accounts;

namespace StadiumEngine.Handlers.Handlers.Accounts;

internal sealed class ToggleRolePermissionHandler : BaseRequestHandler<ToggleRolePermissionCommand, ToggleRolePermissionDto>
{
    private readonly IRolePermissionRepository _rolePermissionRepository;
    private readonly IRoleRepository _roleRepository;
    private readonly IUserRepository _userRepository;

    public ToggleRolePermissionHandler(IMapper mapper, IClaimsIdentityService claimsIdentityService, 
        IUnitOfWork unitOfWork, IRolePermissionRepository rolePermissionRepository, 
        IRoleRepository roleRepository, IUserRepository userRepository) : base(mapper, claimsIdentityService, unitOfWork)
    {
        _rolePermissionRepository = rolePermissionRepository;
        _roleRepository = roleRepository;
        _userRepository = userRepository;
    }
    
    public override async ValueTask<ToggleRolePermissionDto> Handle(ToggleRolePermissionCommand request, CancellationToken cancellationToken)
    {
        var user = await _userRepository.Get(_userId);
        
        if (user?.RoleId == request.RoleId)
            throw new DomainException(ErrorsKeys.ModifyPermissionsCurrentRole);
        
        var role = await _roleRepository.Get(request.RoleId);
        CheckRoleAccess(role);
        
        var rolePermission = await _rolePermissionRepository.Get(request.RoleId, request.PermissionId);
        if (rolePermission == null)
        {
            rolePermission = Mapper.Map<RolePermission>(request);
            rolePermission.UserCreatedId = _userId;
            _rolePermissionRepository.Add(rolePermission);
        }
        else
        {
            _rolePermissionRepository.Remove(rolePermission);
        }

        await UnitOfWork.SaveChanges();

        return new ToggleRolePermissionDto();
    }
}