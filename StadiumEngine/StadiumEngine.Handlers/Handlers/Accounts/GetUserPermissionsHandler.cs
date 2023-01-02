using AutoMapper;
using StadiumEngine.Domain.Repositories.Accounts;
using StadiumEngine.Domain.Services;
using StadiumEngine.Domain.Services.Identity;
using StadiumEngine.DTO.Accounts;
using StadiumEngine.Handlers.Queries.Accounts;

namespace StadiumEngine.Handlers.Handlers.Accounts;

internal sealed class GetUserPermissionsHandler :  BaseRequestHandler<GetUserPermissionsQuery, List<UserPermissionDto>>
{
    private readonly IUserPermissionRepository _repository;

    public GetUserPermissionsHandler(IMapper mapper, IClaimsIdentityService claimsIdentityService, IUnitOfWork unitOfWork, IUserPermissionRepository repository) : base(mapper, claimsIdentityService, unitOfWork)
    {
        _repository = repository;
    }

    public override async ValueTask<List<UserPermissionDto>> Handle(GetUserPermissionsQuery request, CancellationToken cancellationToken)
    {
        var roleId = ClaimsIdentityService.GetUserId();
        var isSuperuser = ClaimsIdentityService.GetIsSuperuser();
        
        var permissions = await _repository.Get(roleId, isSuperuser);

        var permissionsDto = Mapper.Map<List<UserPermissionDto>>(permissions);
        
        return permissionsDto;
    }
}