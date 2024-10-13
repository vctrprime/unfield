using AutoMapper;
using Unfield.Domain.Entities.Accounts;
using Unfield.Domain.Services.Core.Accounts;
using Unfield.Domain.Services.Identity;
using Unfield.DTO.Admin;
using Unfield.Queries.Admin;

namespace Unfield.Handlers.Handlers.Admin;

internal sealed class GetStadiumGroupsByFilterHandler : BaseRequestHandler<GetStadiumGroupsByFilterQuery, List<StadiumGroupDto>>
{
    private readonly IStadiumGroupQueryService _queryService;

    public GetStadiumGroupsByFilterHandler(
        IStadiumGroupQueryService queryService,
        IMapper mapper,
        IClaimsIdentityService claimsIdentityService ) : base( mapper, claimsIdentityService )
    {
        _queryService = queryService;
    }

    public override async ValueTask<List<StadiumGroupDto>> Handle( GetStadiumGroupsByFilterQuery request,
        CancellationToken cancellationToken )
    {
        List<StadiumGroup> stadiumGroups = await _queryService.GetStadiumGroupsByFilterAsync( request.Q ?? String.Empty );

        List<StadiumGroupDto>? stadiumGroupsDto = Mapper.Map<List<StadiumGroupDto>>( stadiumGroups );

        return stadiumGroupsDto;
    }
}