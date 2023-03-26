using StadiumEngine.Domain.Entities.Rates;

namespace StadiumEngine.Domain.Services.Facades.Rates;

public interface ITariffQueryFacade
{
    Task<List<Tariff>> GetByStadiumIdAsync( int stadiumId );
    Task<Tariff?> GetByTariffIdAsync( int tariffId, int stadiumId );
}