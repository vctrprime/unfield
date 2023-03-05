using AutoMapper;
using StadiumEngine.Domain.Entities.Accounts;
using StadiumEngine.Domain.Services.Facades.Accounts;
using StadiumEngine.Domain.Services.Identity;
using StadiumEngine.DTO.Accounts.Users;
using StadiumEngine.Handlers.Queries.Accounts.Users;

namespace StadiumEngine.Handlers.Handlers.Accounts.Users;

internal sealed class GetUsersHandler : BaseRequestHandler<GetUsersQuery, List<UserDto>>
{
    private readonly IUserQueryFacade _userFacade;

    public GetUsersHandler(
        IUserQueryFacade userFacade,
        IMapper mapper,
        IClaimsIdentityService claimsIdentityService ) : base( mapper, claimsIdentityService )
    {
        _userFacade = userFacade;
    }

    public override async ValueTask<List<UserDto>> Handle( GetUsersQuery request, CancellationToken cancellationToken )
    {
        List<User> users = await _userFacade.GetUsersByLegalId( _legalId );

        List<UserDto>? usersDto = Mapper.Map<List<UserDto>>( users );

        return usersDto;
    }
}