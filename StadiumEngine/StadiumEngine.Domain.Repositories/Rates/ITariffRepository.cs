using StadiumEngine.Domain.Entities.Rates;

namespace StadiumEngine.Domain.Repositories.Rates;

public interface ITariffRepository
{
    Task<List<Tariff>> GetAllAsync( int stadiumId );
    Task<Tariff?> GetAsync( int tariffId, int stadiumId );
    void Add( Tariff field );
    void Update( Tariff field );
    void Remove( Tariff field );
}