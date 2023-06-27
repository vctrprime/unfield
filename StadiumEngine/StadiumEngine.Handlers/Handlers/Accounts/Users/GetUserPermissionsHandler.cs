using AutoMapper;
using StadiumEngine.Domain.Entities.Accounts;
using StadiumEngine.Domain.Services.Application.Accounts;
using StadiumEngine.Domain.Services.Identity;
using StadiumEngine.DTO.Accounts.Users;
using StadiumEngine.Queries.Accounts.Users;

namespace StadiumEngine.Handlers.Handlers.Accounts.Users;

internal sealed class GetUserPermissionsHandler : BaseRequestHandler<GetUserPermissionsQuery, List<UserPermissionDto>>
{
    private readonly IUserQueryService _queryService;

    public GetUserPermissionsHandler(
        IUserQueryService queryService,
        IMapper mapper,
        IClaimsIdentityService claimsIdentityService ) : base( mapper, claimsIdentityService )
    {
        _queryService = queryService;
    }

    public override async ValueTask<List<UserPermissionDto>> Handle( GetUserPermissionsQuery request,
        CancellationToken cancellationToken )
    {
        List<Permission> permissions = await _queryService.GetUserPermissionsAsync( _userId );

        List<UserPermissionDto>? permissionsDto = Mapper.Map<List<UserPermissionDto>>( permissions );

        return permissionsDto;
    }
}