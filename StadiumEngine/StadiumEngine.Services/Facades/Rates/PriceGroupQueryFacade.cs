using StadiumEngine.Domain.Entities.Rates;
using StadiumEngine.Domain.Repositories.Rates;
using StadiumEngine.Domain.Services.Facades.Rates;

namespace StadiumEngine.Services.Facades.Rates;

internal class PriceGroupQueryFacade : IPriceGroupQueryFacade
{
    private readonly IPriceGroupRepository _priceGroupRepository;

    public PriceGroupQueryFacade( IPriceGroupRepository priceGroupRepository )
    {
        _priceGroupRepository = priceGroupRepository;
    }

    public async Task<List<PriceGroup>> GetByStadiumIdAsync( int stadiumId ) =>
        await _priceGroupRepository.GetAllAsync( stadiumId );

    public async Task<PriceGroup?> GetByPriceGroupIdAsync( int priceGroupId, int stadiumId ) =>
        await _priceGroupRepository.GetAsync( priceGroupId, stadiumId );
}