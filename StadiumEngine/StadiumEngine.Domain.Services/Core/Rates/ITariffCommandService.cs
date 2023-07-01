using StadiumEngine.Domain.Entities.Rates;

namespace StadiumEngine.Domain.Services.Core.Rates;

public interface ITariffCommandService
{
    Task AddTariffAsync( Tariff tariff, List<string[]> intervals );
    Task UpdateTariffAsync( Tariff tariff, List<string[]> intervals, List<PromoCode> promoCodes );
    Task DeleteTariffAsync( int tariffId, int stadiumId );
}