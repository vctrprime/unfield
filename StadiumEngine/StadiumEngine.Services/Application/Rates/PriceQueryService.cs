using StadiumEngine.Domain.Entities.Rates;
using StadiumEngine.Domain.Repositories.Rates;
using StadiumEngine.Domain.Services.Application.Rates;

namespace StadiumEngine.Services.Application.Rates;

internal class PriceQueryService : IPriceQueryService
{
    private readonly IPriceRepository _repository;

    public PriceQueryService( IPriceRepository repository )
    {
        _repository = repository;
    }
    public Task<List<Price>> GetByStadiumIdAsync( int stadiumId ) => _repository.GetAllAsync( stadiumId );
}