using System.Collections.Generic;

namespace Unfield.Domain.Entities.Rates;

public class TariffDayInterval : BaseUserEntity
{
    public int TariffId { get; set; }
    public virtual Tariff Tariff { get; set; }
    
    public int DayIntervalId { get; set; }
    public virtual DayInterval DayInterval { get; set; }

    public virtual ICollection<Price> Prices { get; set; }
}