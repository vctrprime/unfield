using AutoMapper;
using StadiumEngine.Domain.Entities.Accounts;
using StadiumEngine.Domain.Services.Application.Accounts;
using StadiumEngine.Domain.Services.Identity;
using StadiumEngine.DTO.Accounts.Users;
using StadiumEngine.Queries.Accounts.Users;

namespace StadiumEngine.Handlers.Handlers.Accounts.Users;

internal sealed class GetUserStadiumsHandler : BaseRequestHandler<GetUserStadiumsQuery, List<UserStadiumDto>>
{
    private readonly IUserQueryService _queryService;

    public GetUserStadiumsHandler(
        IUserQueryService queryService,
        IMapper mapper,
        IClaimsIdentityService claimsIdentityService ) : base( mapper, claimsIdentityService )
    {
        _queryService = queryService;
    }

    public override async ValueTask<List<UserStadiumDto>> Handle( GetUserStadiumsQuery request,
        CancellationToken cancellationToken )
    {
        List<Stadium> stadiums = await _queryService.GetUserStadiumsAsync( _userId, _legalId );

        List<UserStadiumDto>? stadiumsDto = Mapper.Map<List<UserStadiumDto>>( stadiums );

        stadiumsDto.First( s => s.Id == _currentStadiumId ).IsCurrent = true;

        return stadiumsDto;
    }
}