using AutoMapper;
using StadiumEngine.Domain.Entities.Accounts;
using StadiumEngine.Domain.Services.Application.Accounts;
using StadiumEngine.Domain.Services.Identity;
using StadiumEngine.DTO.Accounts.Users;
using StadiumEngine.Queries.Accounts.Users;

namespace StadiumEngine.Handlers.Handlers.Accounts.Users;

internal sealed class GetAuthorizedUserHandler : BaseRequestHandler<GetAuthorizedUserQuery, AuthorizedUserDto>
{
    private readonly IUserQueryService _queryService;

    public GetAuthorizedUserHandler(
        IUserQueryService queryService,
        IMapper mapper,
        IClaimsIdentityService claimsIdentityService ) : base( mapper, claimsIdentityService )
    {
        _queryService = queryService;
    }

    public override async ValueTask<AuthorizedUserDto> Handle( GetAuthorizedUserQuery request,
        CancellationToken cancellationToken )
    {
        User? user = await _queryService.GetUserAsync( _userId );

        AuthorizedUserDto? userDto = Mapper.Map<AuthorizedUserDto>( user );

        return userDto;
    }
}