using Unfield.Domain.Entities.Rates;
using Unfield.Domain.Repositories.Rates;
using Unfield.Domain.Services.Core.Rates;

namespace Unfield.Services.Core.Rates;

internal class PriceQueryService : IPriceQueryService
{
    private readonly IPriceRepository _repository;

    public PriceQueryService( IPriceRepository repository )
    {
        _repository = repository;
    }
    public Task<List<Price>> GetByStadiumIdAsync( int stadiumId ) => _repository.GetAllAsync( stadiumId );
}