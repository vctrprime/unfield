using AutoMapper;
using Unfield.Domain.Entities.Accounts;
using Unfield.Domain.Services.Core.Accounts;
using Unfield.Domain.Services.Identity;
using Unfield.DTO.Accounts.Users;
using Unfield.Queries.Accounts.Users;

namespace Unfield.Handlers.Handlers.Accounts.Users;

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