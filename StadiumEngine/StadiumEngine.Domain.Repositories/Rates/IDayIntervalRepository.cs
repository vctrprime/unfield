using StadiumEngine.Domain.Entities.Rates;

namespace StadiumEngine.Domain.Repositories.Rates;

public interface IDayIntervalRepository
{
    Task<DayInterval?> Get( string start, string end );
    void Add( DayInterval dayInterval );
}