using StadiumEngine.Domain.Entities.Rates;
using StadiumEngine.Domain.Repositories.Rates;
using StadiumEngine.Domain.Services.Facades.Rates;

namespace StadiumEngine.Services.Facades.Rates;

internal class PriceQueryFacade : IPriceQueryFacade
{
    private readonly IPriceRepository _repository;

    public PriceQueryFacade( IPriceRepository repository )
    {
        _repository = repository;
    }
    public Task<List<Price>> GetByStadiumIdAsync( int stadiumId ) => _repository.GetAllAsync( stadiumId );
}