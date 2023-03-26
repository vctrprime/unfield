using StadiumEngine.Domain.Entities.Rates;
using StadiumEngine.DTO.Rates.Prices;

namespace StadiumEngine.Handlers.Facades.Rates.Prices;

internal interface ISetPricesFacade
{
    Task<SetPricesDto> SetPricesAsync( IEnumerable<Price> prices, int stadiumId, int userId );
}