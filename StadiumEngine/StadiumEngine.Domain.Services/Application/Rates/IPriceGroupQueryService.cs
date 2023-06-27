using StadiumEngine.Domain.Entities.Rates;

namespace StadiumEngine.Domain.Services.Application.Rates;

public interface IPriceGroupQueryService
{
    Task<List<PriceGroup>> GetByStadiumIdAsync( int stadiumId );
    Task<PriceGroup?> GetByPriceGroupIdAsync( int priceGroupId, int stadiumId );
}