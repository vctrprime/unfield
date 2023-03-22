using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace StadiumEngine.Domain.Entities.Rates;

[Table( "tariff", Schema = "rates" )]
public class Tariff : BaseRateEntity
{
    [Column( "date_start" )]
    public DateTime DateStart { get; set; }

    [Column( "date_end" )]
    public DateTime? DateEnd { get; set; }

    [Column( "monday" )]
    public bool Monday { get; set; }

    [Column( "tuesday" )]
    public bool Tuesday { get; set; }

    [Column( "wednesday" )]
    public bool Wednesday { get; set; }

    [Column( "thursday" )]
    public bool Thursday { get; set; }

    [Column( "friday" )]
    public bool Friday { get; set; }

    [Column( "saturday" )]
    public bool Saturday { get; set; }

    [Column( "sunday" )]
    public bool Sunday { get; set; }

    public virtual ICollection<TariffDayInterval> TariffDayIntervals { get; set; }
    public virtual ICollection<PromoCode> PromoCodes { get; set; }
}