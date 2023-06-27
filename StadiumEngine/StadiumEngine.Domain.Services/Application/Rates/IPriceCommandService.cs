using StadiumEngine.Domain.Entities.Rates;

namespace StadiumEngine.Domain.Services.Application.Rates;

public interface IPriceCommandService
{
    void AddPrices( IEnumerable<Price> prices );
    void DeletePrices( IEnumerable<Price> prices );
}