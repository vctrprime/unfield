using StadiumEngine.Domain.Entities.Rates;

namespace StadiumEngine.Domain.Services.Facades.Rates;

public interface IPriceGroupQueryFacade
{
    Task<List<PriceGroup>> GetByStadiumId( int stadiumId );
    Task<PriceGroup?> GetByPriceGroupId( int priceGroupId, int stadiumId );
}