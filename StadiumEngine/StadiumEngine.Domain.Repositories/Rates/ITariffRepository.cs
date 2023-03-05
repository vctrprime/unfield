using StadiumEngine.Domain.Entities.Rates;

namespace StadiumEngine.Domain.Repositories.Rates;

public interface ITariffRepository
{
    Task<List<Tariff>> GetAll( int stadiumId );
    Task<Tariff?> Get( int rateId, int stadiumId );
    void Add( Tariff field );
    void Update( Tariff field );
    void Remove( Tariff field );
}