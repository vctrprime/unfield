using StadiumEngine.Domain.Entities.Rates;

namespace StadiumEngine.Domain.Services.Facades.Rates;

public interface ITariffCommandFacade
{
    Task AddTariff( Tariff tariff, List<string[]> intervals, IUnitOfWork unitOfWork );
    Task UpdateTariff( Tariff tariff, List<string[]> intervals, List<PromoCode> promoCodes, IUnitOfWork unitOfWork );
    Task DeleteTariff( int tariffId, int stadiumId );
}