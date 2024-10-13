using Unfield.Domain.Entities.Rates;

namespace Unfield.Domain.Repositories.Rates;

public interface IDayIntervalRepository
{
    Task<DayInterval?> GetAsync( string start, string end );
    void Add( DayInterval dayInterval );
}