using StadiumEngine.Domain.Entities.Rates;
using StadiumEngine.Domain.Repositories.Rates;
using StadiumEngine.Domain.Services.Core.Rates;

namespace StadiumEngine.Services.Core.Rates;

internal class TariffQueryService : ITariffQueryService
{
    private readonly ITariffRepository _tariffRepository;

    public TariffQueryService( ITariffRepository tariffRepository )
    {
        _tariffRepository = tariffRepository;
    }

    public async Task<List<Tariff>> GetByStadiumIdAsync( int stadiumId ) => await _tariffRepository.GetAllAsync( stadiumId );

    public async Task<Tariff?> GetByTariffIdAsync( int tariffId, int stadiumId ) =>
        await _tariffRepository.GetAsync( tariffId, stadiumId );
}