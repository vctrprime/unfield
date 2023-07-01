using AutoMapper;
using StadiumEngine.Domain.Entities.Accounts;
using StadiumEngine.Domain.Services.Core.Accounts;
using StadiumEngine.Domain.Services.Identity;
using StadiumEngine.DTO.Accounts.Users;
using StadiumEngine.Queries.Accounts.Users;

namespace StadiumEngine.Handlers.Handlers.Accounts.Users;

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
        List<User> users = await _queryService.GetUsersByLegalIdAsync( _legalId );

        List<UserDto>? usersDto = Mapper.Map<List<UserDto>>( users );

        return usersDto;
    }
}