using AutoMapper;
using StadiumEngine.Domain.Repositories.Accounts;
using StadiumEngine.Domain.Services.Identity;
using StadiumEngine.DTO.Accounts;
using StadiumEngine.Handlers.Queries.Accounts;

namespace StadiumEngine.Handlers.Handlers.Accounts;

internal sealed class GetUserPermissionsHandler :  BaseRequestHandler<GetUserPermissionsQuery, List<UserPermissionDto>>
{
    private readonly IUserPermissionRepository _repository;

    public GetUserPermissionsHandler(IMapper mapper, IClaimsIdentityService claimsIdentityService, IUserPermissionRepository repository) : base(mapper, claimsIdentityService)
    {
        _repository = repository;
    }
    
    public override async ValueTask<List<UserPermissionDto>> Handle(GetUserPermissionsQuery request, CancellationToken cancellationToken)
    {
        var userId = ClaimsIdentityService.GetUserId();
        var permissions = await _repository.Get(userId);

        var permissionsDto = Mapper.Map<List<UserPermissionDto>>(permissions);
        
        return permissionsDto;
    }
}