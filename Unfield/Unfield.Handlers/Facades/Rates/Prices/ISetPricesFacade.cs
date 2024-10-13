using Unfield.Domain.Entities.Rates;
using Unfield.DTO.Rates.Prices;

namespace Unfield.Handlers.Facades.Rates.Prices;

internal interface ISetPricesFacade
{
    Task<SetPricesDto> SetPricesAsync( IEnumerable<Price> prices, int stadiumId, int userId );
}