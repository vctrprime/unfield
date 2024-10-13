using AutoMapper;
using Unfield.Domain.Entities.Accounts;
using Unfield.Domain.Services.Core.Accounts;
using Unfield.Domain.Services.Identity;
using Unfield.DTO.Accounts.Users;
using Unfield.Queries.Accounts.Users;

namespace Unfield.Handlers.Handlers.Accounts.Users;

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