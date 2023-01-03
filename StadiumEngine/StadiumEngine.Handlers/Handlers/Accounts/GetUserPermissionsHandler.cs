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
    private readonly IPermissionRepository _repository;

    public GetUserPermissionsHandler(IMapper mapper, IClaimsIdentityService claimsIdentityService, IUnitOfWork unitOfWork, IPermissionRepository repository) : base(mapper, claimsIdentityService, unitOfWork)
    {
        _repository = repository;
    }

    public override async ValueTask<List<UserPermissionDto>> Handle(GetUserPermissionsQuery request, CancellationToken cancellationToken)
    {
        var isSuperuser = ClaimsIdentityService.GetIsSuperuser();

        List<Permission> permissions;
        if (isSuperuser)
        {
            permissions = await _repository.GetAll();
        }
        else
        {
            var roleId = ClaimsIdentityService.GetRoleId();
            permissions = await _repository.GetForRole(roleId);
        }
        
        var permissionsDto = Mapper.Map<List<UserPermissionDto>>(permissions);
        
        return permissionsDto;
    }
}