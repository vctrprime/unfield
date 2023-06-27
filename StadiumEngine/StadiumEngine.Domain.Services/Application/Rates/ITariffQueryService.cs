using StadiumEngine.Domain.Entities.Rates;

namespace StadiumEngine.Domain.Services.Application.Rates;

public interface ITariffQueryService
{
    Task<List<Tariff>> GetByStadiumIdAsync( int stadiumId );
    Task<Tariff?> GetByTariffIdAsync( int tariffId, int stadiumId );
}