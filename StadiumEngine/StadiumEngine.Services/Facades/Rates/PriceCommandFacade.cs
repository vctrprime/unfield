using StadiumEngine.Domain.Entities.Rates;
using StadiumEngine.Domain.Repositories.Rates;
using StadiumEngine.Domain.Services.Facades.Rates;

namespace StadiumEngine.Services.Facades.Rates;

internal class PriceCommandFacade : IPriceCommandFacade
{
    private readonly IPriceRepository _repository;

    public PriceCommandFacade( IPriceRepository repository )
    {
        _repository = repository;
    }
    public void AddPrices( IEnumerable<Price> prices ) => _repository.Add( prices );

    public void DeletePrices( IEnumerable<Price> prices ) => _repository.Remove( prices );
}