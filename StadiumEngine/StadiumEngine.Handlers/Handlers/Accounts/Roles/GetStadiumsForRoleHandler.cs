using AutoMapper;
using StadiumEngine.Domain.Entities.Accounts;
using StadiumEngine.Domain.Services.Facades.Accounts;
using StadiumEngine.Domain.Services.Identity;
using StadiumEngine.DTO.Accounts.Stadiums;
using StadiumEngine.Handlers.Queries.Accounts.Roles;

namespace StadiumEngine.Handlers.Handlers.Accounts.Roles;

internal sealed class GetStadiumsForRoleHandler : BaseRequestHandler<GetStadiumsForRoleQuery, List<StadiumDto>>
{
    private readonly IRoleQueryFacade _roleFacade;

    public GetStadiumsForRoleHandler(
        IRoleQueryFacade roleFacade,
        IMapper mapper,
        IClaimsIdentityService claimsIdentityService ) : base( mapper, claimsIdentityService )
    {
        _roleFacade = roleFacade;
    }

    public override async ValueTask<List<StadiumDto>> Handle( GetStadiumsForRoleQuery request,
        CancellationToken cancellationToken )
    {
        Dictionary<Stadium, bool> stadiums = await _roleFacade.GetStadiumsForRole( request.RoleId, _legalId );

        List<StadiumDto>? stadiumsDto = Mapper.Map<List<StadiumDto>>( stadiums.Keys );
        stadiumsDto.ForEach( sd => { sd.IsRoleBound = stadiums.FirstOrDefault( s => s.Key.Id == sd.Id ).Value; } );

        return stadiumsDto;
    }
}