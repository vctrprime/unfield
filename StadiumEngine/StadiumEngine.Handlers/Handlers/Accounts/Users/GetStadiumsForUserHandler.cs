using AutoMapper;
using StadiumEngine.Domain.Entities.Accounts;
using StadiumEngine.Domain.Services.Core.Accounts;
using StadiumEngine.Domain.Services.Identity;
using StadiumEngine.DTO.Accounts.Stadiums;
using StadiumEngine.Queries.Accounts.Roles;
using StadiumEngine.Queries.Accounts.Users;

namespace StadiumEngine.Handlers.Handlers.Accounts.Users;

internal sealed class GetStadiumsForRoleHandler : BaseRequestHandler<GetStadiumsForUserQuery, List<StadiumDto>>
{
    private readonly IUserQueryService _queryService;

    public GetStadiumsForRoleHandler(
        IUserQueryService queryService,
        IMapper mapper,
        IClaimsIdentityService claimsIdentityService ) : base( mapper, claimsIdentityService )
    {
        _queryService = queryService;
    }

    public override async ValueTask<List<StadiumDto>> Handle( GetStadiumsForUserQuery request,
        CancellationToken cancellationToken )
    {
        Dictionary<Stadium, bool> stadiums = await _queryService.GetStadiumsForUserAsync( request.UserId, _stadiumGroupId );

        List<StadiumDto>? stadiumsDto = Mapper.Map<List<StadiumDto>>( stadiums.Keys );
        stadiumsDto.ForEach( sd => { sd.IsUserBound = stadiums.FirstOrDefault( s => s.Key.Id == sd.Id ).Value; } );

        return stadiumsDto;
    }
}