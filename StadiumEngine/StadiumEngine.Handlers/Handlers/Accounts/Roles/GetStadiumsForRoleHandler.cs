using AutoMapper;
using StadiumEngine.Domain.Entities.Accounts;
using StadiumEngine.Domain.Services.Core.Accounts;
using StadiumEngine.Domain.Services.Identity;
using StadiumEngine.DTO.Accounts.Stadiums;
using StadiumEngine.Queries.Accounts.Roles;

namespace StadiumEngine.Handlers.Handlers.Accounts.Roles;

internal sealed class GetStadiumsForRoleHandler : BaseRequestHandler<GetStadiumsForRoleQuery, List<StadiumDto>>
{
    private readonly IRoleQueryService _queryService;

    public GetStadiumsForRoleHandler(
        IRoleQueryService queryService,
        IMapper mapper,
        IClaimsIdentityService claimsIdentityService ) : base( mapper, claimsIdentityService )
    {
        _queryService = queryService;
    }

    public override async ValueTask<List<StadiumDto>> Handle( GetStadiumsForRoleQuery request,
        CancellationToken cancellationToken )
    {
        Dictionary<Stadium, bool> stadiums = await _queryService.GetStadiumsForRoleAsync( request.RoleId, _legalId );

        List<StadiumDto>? stadiumsDto = Mapper.Map<List<StadiumDto>>( stadiums.Keys );
        stadiumsDto.ForEach( sd => { sd.IsRoleBound = stadiums.FirstOrDefault( s => s.Key.Id == sd.Id ).Value; } );

        return stadiumsDto;
    }
}