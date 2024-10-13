using AutoMapper;
using Unfield.Domain.Entities.Rates;
using Unfield.Domain.Services.Core.Rates;
using Unfield.Domain.Services.Identity;
using Unfield.DTO.Rates.PriceGroups;
using Unfield.Queries.Rates.PriceGroups;

namespace Unfield.Handlers.Handlers.Rates.PriceGroups;

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