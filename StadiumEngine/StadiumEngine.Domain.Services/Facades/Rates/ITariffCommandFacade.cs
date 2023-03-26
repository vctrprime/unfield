using StadiumEngine.Domain.Entities.Rates;

namespace StadiumEngine.Domain.Services.Facades.Rates;

public interface ITariffCommandFacade
{
    Task AddTariffAsync( Tariff tariff, List<string[]> intervals );
    Task UpdateTariffAsync( Tariff tariff, List<string[]> intervals, List<PromoCode> promoCodes );
    Task DeleteTariffAsync( int tariffId, int stadiumId );
}