using AutoMapper;
using StadiumEngine.Domain;
using StadiumEngine.Domain.Services;
using StadiumEngine.Domain.Services.Facades.Accounts;
using StadiumEngine.Domain.Services.Identity;
using StadiumEngine.DTO.Admin;
using StadiumEngine.Handlers.Queries.Admin;

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
        var legals = await _legalFacade.GetLegalsByFilter( request.SearchString );

        var legalsDto = Mapper.Map<List<LegalDto>>( legals );

        return legalsDto;
    }
}