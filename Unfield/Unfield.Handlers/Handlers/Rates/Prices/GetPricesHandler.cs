using AutoMapper;
using Unfield.Domain.Entities.Rates;
using Unfield.Domain.Services.Core.Rates;
using Unfield.Domain.Services.Identity;
using Unfield.DTO.Rates.Prices;
using Unfield.Queries.Rates.Prices;

namespace Unfield.Handlers.Handlers.Rates.Prices;

internal sealed class GetPricesHandler : BaseRequestHandler<GetPricesQuery, List<PriceDto>>
{
    private readonly IPriceQueryService _queryService;

    public GetPricesHandler(
        IPriceQueryService queryService,
        IMapper mapper,
        IClaimsIdentityService claimsIdentityService ) : base( mapper, claimsIdentityService )
    {
        _queryService = queryService;
    }

    public override async ValueTask<List<PriceDto>> Handle( GetPricesQuery request,
        CancellationToken cancellationToken )
    {
        List<Price> prices = await _queryService.GetByStadiumIdAsync( _currentStadiumId );

        List<PriceDto> pricesDto = Mapper.Map<List<PriceDto>>( prices );

        return pricesDto;
    }
}