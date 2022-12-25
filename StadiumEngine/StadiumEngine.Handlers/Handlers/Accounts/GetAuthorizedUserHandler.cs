using AutoMapper;
using StadiumEngine.DTO.Accounts;
using StadiumEngine.Handlers.Queries.Accounts;
using StadiumEngine.Services.Auth.Abstract;

namespace StadiumEngine.Handlers.Handlers.Accounts;

internal sealed class GetAuthorizedUserHandler : BaseRequestHandler<GetAuthorizedUserQuery, AuthorizeUserDto>
{
    public GetAuthorizedUserHandler(IMapper mapper, IClaimsIdentityService claimsIdentityService) : base(mapper, claimsIdentityService)
    {
    }

    public override async ValueTask<AuthorizeUserDto> Handle(GetAuthorizedUserQuery request, CancellationToken cancellationToken)
    {
        var user = new AuthorizeUserDto
        {
            FullName = ClaimsIdentityService.GetUserName(),
            IsSuperuser = ClaimsIdentityService.GetIsSuperuser(),
            RoleName = ClaimsIdentityService.GetRoleName()
        };

        return user;
    }
}