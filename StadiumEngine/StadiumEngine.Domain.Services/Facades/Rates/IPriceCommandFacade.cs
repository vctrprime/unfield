using StadiumEngine.Domain.Entities.Rates;

namespace StadiumEngine.Domain.Services.Facades.Rates;

public interface IPriceCommandFacade
{
    void AddPrices( IEnumerable<Price> prices );
    void DeletePrices( IEnumerable<Price> prices );
}