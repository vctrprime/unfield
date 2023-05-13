using AutoMapper;
using StadiumEngine.Domain.Entities.Accounts;
using StadiumEngine.Domain.Services.Facades.Accounts;
using StadiumEngine.Domain.Services.Identity;
using StadiumEngine.DTO.Admin;
using StadiumEngine.Queries.Admin;

namespace StadiumEngine.Handlers.Handlers.Admin;

internal sealed class GetLegalsByFilterHandler : BaseRequestHandler<GetLegalsByFilterQuery, List<LegalDto>>
{
    private readonly ILegalQueryFacade _legalFacade;

    public GetLegalsByFilterHandler(
        ILegalQueryFacade legalFacade,
        IMapper mapper,
        IClaimsIdentityService claimsIdentityService ) : base( mapper, claimsIdentityService )
    {
        _legalFacade = legalFacade;
    }

    public override async ValueTask<List<LegalDto>> Handle( GetLegalsByFilterQuery request,
        CancellationToken cancellationToken )
    {
        List<Legal> legals = await _legalFacade.GetLegalsByFilterAsync( request.Q ?? String.Empty );

        List<LegalDto>? legalsDto = Mapper.Map<List<LegalDto>>( legals );

        return legalsDto;
    }
}