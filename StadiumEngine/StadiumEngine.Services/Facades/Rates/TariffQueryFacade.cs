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

    public async Task<List<Tariff>> GetByStadiumId( int stadiumId ) => await _tariffRepository.GetAll( stadiumId );

    public async Task<Tariff?> GetByTariffId( int tariffId, int stadiumId ) =>
        await _tariffRepository.Get( tariffId, stadiumId );
}