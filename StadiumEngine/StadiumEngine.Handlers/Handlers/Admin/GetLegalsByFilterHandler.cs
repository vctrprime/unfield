using AutoMapper;
using StadiumEngine.Domain.Entities.Accounts;
using StadiumEngine.Domain.Services.Core.Accounts;
using StadiumEngine.Domain.Services.Identity;
using StadiumEngine.DTO.Admin;
using StadiumEngine.Queries.Admin;

namespace StadiumEngine.Handlers.Handlers.Admin;

internal sealed class GetLegalsByFilterHandler : BaseRequestHandler<GetLegalsByFilterQuery, List<LegalDto>>
{
    private readonly ILegalQueryService _queryService;

    public GetLegalsByFilterHandler(
        ILegalQueryService queryService,
        IMapper mapper,
        IClaimsIdentityService claimsIdentityService ) : base( mapper, claimsIdentityService )
    {
        _queryService = queryService;
    }

    public override async ValueTask<List<LegalDto>> Handle( GetLegalsByFilterQuery request,
        CancellationToken cancellationToken )
    {
        List<Legal> legals = await _queryService.GetLegalsByFilterAsync( request.Q ?? String.Empty );

        List<LegalDto>? legalsDto = Mapper.Map<List<LegalDto>>( legals );

        return legalsDto;
    }
}