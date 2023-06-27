using StadiumEngine.Domain.Entities.Rates;

namespace StadiumEngine.Domain.Services.Application.Rates;

public interface IPriceGroupCommandService
{
    void AddPriceGroup( PriceGroup priceGroup );
    void UpdatePriceGroup( PriceGroup priceGroup );
    Task DeletePriceGroupAsync( int priceGroupId, int stadiumId );
}