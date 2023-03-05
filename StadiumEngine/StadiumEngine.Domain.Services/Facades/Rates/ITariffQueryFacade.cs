using StadiumEngine.Domain.Entities.Rates;

namespace StadiumEngine.Domain.Services.Facades.Rates;

public interface ITariffQueryFacade
{
    Task<List<Tariff>> GetByStadiumId( int stadiumId );
    Task<Tariff?> GetByTariffId( int tariffId, int stadiumId );
}