using StadiumEngine.Domain.Entities.Rates;
using StadiumEngine.Domain.Repositories.Rates;
using StadiumEngine.Domain.Services.Facades.Rates;

namespace StadiumEngine.Services.Facades.Rates;

internal class TariffQueryFacade : ITariffQueryFacade
{
    private readonly ITariffRepository _tariffRepository;

    public TariffQueryFacade( ITariffRepository tariffRepository )
    {
        _tariffRepository = tariffRepository;
    }

    public async Task<List<Tariff>> GetByStadiumIdAsync( int stadiumId ) => await _tariffRepository.GetAllAsync( stadiumId );

    public async Task<Tariff?> GetByTariffIdAsync( int tariffId, int stadiumId ) =>
        await _tariffRepository.GetAsync( tariffId, stadiumId );
}