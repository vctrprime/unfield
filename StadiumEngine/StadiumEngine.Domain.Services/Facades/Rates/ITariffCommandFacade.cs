using StadiumEngine.Domain.Entities.Rates;

namespace StadiumEngine.Domain.Services.Facades.Rates;

public interface ITariffCommandFacade
{
    Task AddTariffAsync( Tariff tariff, List<string[]> intervals, IUnitOfWork unitOfWork );
    Task UpdateTariffAsync( Tariff tariff, List<string[]> intervals, List<PromoCode> promoCodes, IUnitOfWork unitOfWork );
    Task DeleteTariffAsync( int tariffId, int stadiumId );
}