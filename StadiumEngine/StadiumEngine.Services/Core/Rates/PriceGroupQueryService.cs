using StadiumEngine.Domain.Entities.Rates;
using StadiumEngine.Domain.Repositories.Rates;
using StadiumEngine.Domain.Services.Core.Rates;

namespace StadiumEngine.Services.Core.Rates;

internal class PriceGroupQueryService : IPriceGroupQueryService
{
    private readonly IPriceGroupRepository _priceGroupRepository;

    public PriceGroupQueryService( IPriceGroupRepository priceGroupRepository )
    {
        _priceGroupRepository = priceGroupRepository;
    }

    public async Task<List<PriceGroup>> GetByStadiumIdAsync( int stadiumId ) =>
        await _priceGroupRepository.GetAllAsync( stadiumId );

    public async Task<PriceGroup?> GetByPriceGroupIdAsync( int priceGroupId, int stadiumId ) =>
        await _priceGroupRepository.GetAsync( priceGroupId, stadiumId );
}