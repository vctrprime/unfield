using AutoMapper;
using StadiumEngine.Domain.Entities.Rates;
using StadiumEngine.Domain.Services.Facades.Rates;
using StadiumEngine.Domain.Services.Identity;
using StadiumEngine.DTO.Rates.Prices;
using StadiumEngine.Queries.Rates.Prices;

namespace StadiumEngine.Handlers.Handlers.Rates.Prices;

internal sealed class GetPricesHandler : BaseRequestHandler<GetPricesQuery, List<PriceDto>>
{
    private readonly IPriceQueryFacade _priceFacade;

    public GetPricesHandler(
        IPriceQueryFacade priceFacade,
        IMapper mapper,
        IClaimsIdentityService claimsIdentityService ) : base( mapper, claimsIdentityService )
    {
        _priceFacade = priceFacade;
    }

    public override async ValueTask<List<PriceDto>> Handle( GetPricesQuery request,
        CancellationToken cancellationToken )
    {
        List<Price> prices = await _priceFacade.GetByStadiumId( _currentStadiumId );

        List<PriceDto> pricesDto = Mapper.Map<List<PriceDto>>( prices );

        return pricesDto;
    }
}