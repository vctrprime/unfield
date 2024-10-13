using Unfield.Domain.Entities.Rates;

namespace Unfield.Domain.Services.Core.Rates;

public interface IPriceCommandService
{
    void AddPrices( IEnumerable<Price> prices );
    void DeletePrices( IEnumerable<Price> prices );
}