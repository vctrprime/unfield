using Unfield.Domain.Entities.Rates;
using Unfield.Domain.Repositories.Rates;
using Unfield.Domain.Services.Core.Rates;

namespace Unfield.Services.Core.Rates;

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