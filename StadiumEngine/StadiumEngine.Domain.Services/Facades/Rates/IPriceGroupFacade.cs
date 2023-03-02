using StadiumEngine.Domain.Entities.Rates;

namespace StadiumEngine.Domain.Services.Facades.Rates;

public interface IPriceGroupFacade
{
    Task<List<PriceGroup>> GetByStadiumId(int stadiumId);
    Task<PriceGroup?> GetByPriceGroupId(int priceGroupId, int stadiumId);
    void AddPriceGroup(PriceGroup priceGroup);
    void UpdatePriceGroup(PriceGroup priceGroup);
    Task DeletePriceGroup(int priceGroupId, int stadiumId);
}