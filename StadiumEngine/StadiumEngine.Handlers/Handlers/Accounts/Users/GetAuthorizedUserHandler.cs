using AutoMapper;
using StadiumEngine.Domain;
using StadiumEngine.Domain.Services;
using StadiumEngine.Domain.Services.Facades.Accounts;
using StadiumEngine.Domain.Services.Identity;
using StadiumEngine.DTO.Accounts.Users;
using StadiumEngine.Handlers.Queries.Accounts.Users;

namespace StadiumEngine.Handlers.Handlers.Accounts.Users;

internal sealed class GetAuthorizedUserHandler : BaseRequestHandler<GetAuthorizedUserQuery, AuthorizedUserDto>
{
    private readonly IUserQueryFacade _userFacade;
    
    public GetAuthorizedUserHandler(
        IUserQueryFacade userFacade,
        IMapper mapper, 
        IClaimsIdentityService claimsIdentityService) : base(mapper, claimsIdentityService)
    {
        _userFacade = userFacade;
    }

    public override async ValueTask<AuthorizedUserDto> Handle(GetAuthorizedUserQuery request, CancellationToken cancellationToken)
    {
        var user = await _userFacade.GetUser(_userId);

        var userDto = Mapper.Map<AuthorizedUserDto>(user);

        return userDto;
    }
}