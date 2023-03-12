using AutoMapper;
using StadiumEngine.Domain.Entities.Accounts;
using StadiumEngine.Domain.Services.Facades.Accounts;
using StadiumEngine.Domain.Services.Identity;
using StadiumEngine.DTO.Accounts.Users;
using StadiumEngine.Queries.Accounts.Users;

namespace StadiumEngine.Handlers.Handlers.Accounts.Users;

internal sealed class GetAuthorizedUserHandler : BaseRequestHandler<GetAuthorizedUserQuery, AuthorizedUserDto>
{
    private readonly IUserQueryFacade _userFacade;

    public GetAuthorizedUserHandler(
        IUserQueryFacade userFacade,
        IMapper mapper,
        IClaimsIdentityService claimsIdentityService ) : base( mapper, claimsIdentityService )
    {
        _userFacade = userFacade;
    }

    public override async ValueTask<AuthorizedUserDto> Handle( GetAuthorizedUserQuery request,
        CancellationToken cancellationToken )
    {
        User? user = await _userFacade.GetUser( _userId );

        AuthorizedUserDto? userDto = Mapper.Map<AuthorizedUserDto>( user );

        return userDto;
    }
}