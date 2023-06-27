using AutoMapper;
using StadiumEngine.Domain.Entities.Rates;
using StadiumEngine.Domain.Services.Application.Rates;
using StadiumEngine.Domain.Services.Identity;
using StadiumEngine.DTO.Rates.Prices;
using StadiumEngine.Queries.Rates.Prices;

namespace StadiumEngine.Handlers.Handlers.Rates.Prices;

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