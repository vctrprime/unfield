using Unfield.Domain.Entities.Rates;

namespace Unfield.Domain.Services.Core.Rates;

public interface IPriceGroupCommandService
{
    void AddPriceGroup( PriceGroup priceGroup );
    void UpdatePriceGroup( PriceGroup priceGroup );
    Task DeletePriceGroupAsync( int priceGroupId, int stadiumId );
}