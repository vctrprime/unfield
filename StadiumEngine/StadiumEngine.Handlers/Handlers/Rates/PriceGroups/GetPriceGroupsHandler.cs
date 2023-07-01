using AutoMapper;
using StadiumEngine.Domain.Entities.Rates;
using StadiumEngine.Domain.Services.Core.Rates;
using StadiumEngine.Domain.Services.Identity;
using StadiumEngine.DTO.Rates.PriceGroups;
using StadiumEngine.Queries.Rates.PriceGroups;

namespace StadiumEngine.Handlers.Handlers.Rates.PriceGroups;

internal sealed class GetPriceGroupsHandler : BaseRequestHandler<GetPriceGroupsQuery, List<PriceGroupDto>>
{
    private readonly IPriceGroupQueryService _queryService;

    public GetPriceGroupsHandler(
        IPriceGroupQueryService queryService,
        IMapper mapper,
        IClaimsIdentityService claimsIdentityService ) : base( mapper, claimsIdentityService )
    {
        _queryService = queryService;
    }

    public override async ValueTask<List<PriceGroupDto>> Handle( GetPriceGroupsQuery request,
        CancellationToken cancellationToken )
    {
        List<PriceGroup> priceGroups = await _queryService.GetByStadiumIdAsync( _currentStadiumId );

        List<PriceGroupDto>? priceGroupsDto = Mapper.Map<List<PriceGroupDto>>( priceGroups );

        return priceGroupsDto;
    }
}