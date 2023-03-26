using StadiumEngine.Domain.Entities.Rates;

namespace StadiumEngine.Domain.Services.Facades.Rates;

public interface IPriceGroupCommandFacade
{
    void AddPriceGroup( PriceGroup priceGroup );
    void UpdatePriceGroup( PriceGroup priceGroup );
    Task DeletePriceGroupAsync( int priceGroupId, int stadiumId );
}