using Unfield.Domain.Entities.Rates;

namespace Unfield.Domain.Services.Core.Rates;

public interface IPriceGroupQueryService
{
    Task<List<PriceGroup>> GetByStadiumIdAsync( int stadiumId );
    Task<PriceGroup?> GetByPriceGroupIdAsync( int priceGroupId, int stadiumId );
}