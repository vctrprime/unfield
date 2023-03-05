using StadiumEngine.Domain.Entities.Rates;

namespace StadiumEngine.Domain.Repositories.Rates;

public interface ITariffDayIntervalRepository
{
    void Add( TariffDayInterval tariffDayInterval );
    void Remove( IEnumerable<TariffDayInterval> tariffDayIntervals );
}