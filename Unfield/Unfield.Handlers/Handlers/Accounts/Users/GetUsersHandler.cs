using AutoMapper;
using Unfield.Domain.Entities.Accounts;
using Unfield.Domain.Services.Core.Accounts;
using Unfield.Domain.Services.Identity;
using Unfield.DTO.Accounts.Users;
using Unfield.Queries.Accounts.Users;

namespace Unfield.Handlers.Handlers.Accounts.Users;

internal sealed class GetUsersHandler : BaseRequestHandler<GetUsersQuery, List<UserDto>>
{
    private readonly IUserQueryService _queryService;

    public GetUsersHandler(
        IUserQueryService queryService,
        IMapper mapper,
        IClaimsIdentityService claimsIdentityService ) : base( mapper, claimsIdentityService )
    {
        _queryService = queryService;
    }

    public override async ValueTask<List<UserDto>> Handle( GetUsersQuery request, CancellationToken cancellationToken )
    {
        List<User> users = await _queryService.GetUsersByStadiumGroupIdAsync( _stadiumGroupId );

        List<UserDto>? usersDto = Mapper.Map<List<UserDto>>( users );

        return usersDto;
    }
}