using System.Collections.Generic;

namespace StadiumEngine.Domain.Entities.Rates;

public class DayInterval : BaseEntity
{
    public string Start { get; set; }
    public string End { get; set; }

    public virtual ICollection<TariffDayInterval> TariffDayIntervals { get; set; }
}