using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace StadiumEngine.Domain.Entities.Rates;

[Table( "tariff_day_interval", Schema = "rates" )]
public class TariffDayInterval : BaseRefEntity
{
    [Column( "tariff_id" )] public int TariffId { get; set; }

    [ForeignKey( "TariffId" )] public virtual Tariff Tariff { get; set; }

    [Column( "day_interval_id" )] public int DayIntervalId { get; set; }

    [ForeignKey( "DayIntervalId" )] public virtual DayInterval DayInterval { get; set; }

    public virtual ICollection<Price> Prices { get; set; }
}