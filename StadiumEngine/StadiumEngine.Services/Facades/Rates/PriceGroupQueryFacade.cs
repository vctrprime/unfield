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

    public async Task<List<PriceGroup>> GetByStadiumId( int stadiumId ) =>
        await _priceGroupRepository.GetAll( stadiumId );

    public async Task<PriceGroup?> GetByPriceGroupId( int priceGroupId, int stadiumId ) =>
        await _priceGroupRepository.Get( priceGroupId, stadiumId );
}