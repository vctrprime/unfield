using AutoMapper;
using StadiumEngine.Domain.Entities.Accounts;
using StadiumEngine.Domain.Repositories.Accounts;
using StadiumEngine.Domain.Services;
using StadiumEngine.Domain.Services.Identity;
using StadiumEngine.DTO.Accounts;
using StadiumEngine.Handlers.Queries.Accounts;

namespace StadiumEngine.Handlers.Handlers.Accounts;

internal sealed class GetUserPermissionsHandler :  BaseRequestHandler<GetUserPermissionsQuery, List<UserPermissionDto>>
{
    private readonly IPermissionRepository _permissionRepository;
    private readonly IUserRepository _userRepository;

    public GetUserPermissionsHandler(IMapper mapper, IClaimsIdentityService claimsIdentityService,
        IUnitOfWork unitOfWork, IPermissionRepository permissionRepository,
        IUserRepository userRepository) : base(mapper, claimsIdentityService, unitOfWork)
    {
        _permissionRepository = permissionRepository;
        _userRepository = userRepository;
    }

    public override async ValueTask<List<UserPermissionDto>> Handle(GetUserPermissionsQuery request, CancellationToken cancellationToken)
    {
        var user = await _userRepository.Get(_userId);

        List<Permission> permissions = new List<Permission>();
        
        switch (user)
        {
            case { IsSuperuser: true, Role: null }:
                permissions = await _permissionRepository.GetAll();
                break;
            case { Role: { } }:
                permissions = await _permissionRepository.GetForRole(user.Role.Id);
                break;
        }
        
        var permissionsDto = Mapper.Map<List<UserPermissionDto>>(permissions);
        
        return permissionsDto;
    }
}