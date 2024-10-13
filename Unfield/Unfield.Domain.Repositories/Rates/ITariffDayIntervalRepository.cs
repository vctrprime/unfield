using Unfield.Domain.Entities.Rates;

namespace Unfield.Domain.Repositories.Rates;

public interface ITariffDayIntervalRepository
{
    void Add( TariffDayInterval tariffDayInterval );
    void Remove( IEnumerable<TariffDayInterval> tariffDayIntervals );
}