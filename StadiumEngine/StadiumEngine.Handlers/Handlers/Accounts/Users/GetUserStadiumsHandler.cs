using AutoMapper;
using StadiumEngine.Domain;
using StadiumEngine.Domain.Services;
using StadiumEngine.Domain.Services.Facades.Accounts;
using StadiumEngine.Domain.Services.Identity;
using StadiumEngine.DTO.Accounts.Users;
using StadiumEngine.Handlers.Queries.Accounts.Users;

namespace StadiumEngine.Handlers.Handlers.Accounts.Users;

internal sealed class GetUserStadiumsHandler :  BaseRequestHandler<GetUserStadiumsQuery, List<UserStadiumDto>>
{
    private readonly IUserQueryFacade _userFacade;

    public GetUserStadiumsHandler(
        IUserQueryFacade userFacade,
        IMapper mapper, 
        IClaimsIdentityService claimsIdentityService) : base(mapper, claimsIdentityService)
    {
        _userFacade = userFacade;
    }

    public override async ValueTask<List<UserStadiumDto>> Handle(GetUserStadiumsQuery request, CancellationToken cancellationToken)
    {
        var stadiums = await _userFacade.GetUserStadiums(_userId, _legalId);
        
        var stadiumsDto = Mapper.Map<List<UserStadiumDto>>(stadiums);
        
        stadiumsDto.First(s => s.Id == _currentStadiumId).IsCurrent = true;

        return stadiumsDto;
    }
}