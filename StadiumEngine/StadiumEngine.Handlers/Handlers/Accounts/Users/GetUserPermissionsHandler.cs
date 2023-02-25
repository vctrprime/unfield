using AutoMapper;
using StadiumEngine.Domain;
using StadiumEngine.Domain.Services;
using StadiumEngine.Domain.Services.Facades.Accounts;
using StadiumEngine.Domain.Services.Identity;
using StadiumEngine.DTO.Accounts.Users;
using StadiumEngine.Handlers.Queries.Accounts.Users;

namespace StadiumEngine.Handlers.Handlers.Accounts.Users;

internal sealed class GetUserPermissionsHandler :  BaseRequestHandler<GetUserPermissionsQuery, List<UserPermissionDto>>
{
    private readonly IUserFacade _userFacade;
    
    public GetUserPermissionsHandler(
        IUserFacade userFacade,
        IMapper mapper, 
        IClaimsIdentityService claimsIdentityService,
        IUnitOfWork unitOfWork) : base(mapper, claimsIdentityService, unitOfWork)
    {
        _userFacade = userFacade;
    }

    public override async ValueTask<List<UserPermissionDto>> Handle(GetUserPermissionsQuery request, CancellationToken cancellationToken)
    {
        var permissions = await _userFacade.GetUserPermissions(_userId);
        
        var permissionsDto = Mapper.Map<List<UserPermissionDto>>(permissions);
        
        return permissionsDto;
    }
}