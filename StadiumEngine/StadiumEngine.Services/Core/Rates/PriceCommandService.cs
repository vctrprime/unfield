using StadiumEngine.Domain.Entities.Rates;
using StadiumEngine.Domain.Repositories.Rates;
using StadiumEngine.Domain.Services.Core.Rates;

namespace StadiumEngine.Services.Core.Rates;

internal class PriceCommandService : IPriceCommandService
{
    private readonly IPriceRepository _repository;

    public PriceCommandService( IPriceRepository repository )
    {
        _repository = repository;
    }
    public void AddPrices( IEnumerable<Price> prices ) => _repository.Add( prices );

    public void DeletePrices( IEnumerable<Price> prices ) => _repository.Remove( prices );
}