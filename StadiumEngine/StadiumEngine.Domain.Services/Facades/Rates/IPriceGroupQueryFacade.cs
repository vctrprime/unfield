using StadiumEngine.Domain.Entities.Rates;

namespace StadiumEngine.Domain.Services.Facades.Rates;

public interface IPriceGroupQueryFacade
{
    Task<List<PriceGroup>> GetByStadiumIdAsync( int stadiumId );
    Task<PriceGroup?> GetByPriceGroupIdAsync( int priceGroupId, int stadiumId );
}