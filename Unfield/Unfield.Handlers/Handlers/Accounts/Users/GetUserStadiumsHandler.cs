using AutoMapper;
using Unfield.Domain.Entities.Accounts;
using Unfield.Domain.Services.Core.Accounts;
using Unfield.Domain.Services.Identity;
using Unfield.DTO.Accounts.Users;
using Unfield.Queries.Accounts.Users;

namespace Unfield.Handlers.Handlers.Accounts.Users;

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
        List<Stadium> stadiums = await _queryService.GetUserStadiumsAsync( _userId, _stadiumGroupId );

        List<UserStadiumDto> stadiumsDto = Mapper.Map<List<UserStadiumDto>>( stadiums );

        stadiumsDto.First( s => s.Id == _currentStadiumId ).IsCurrent = true;

        return stadiumsDto;
    }
}