using System;
using System.Collections.Generic;
using StadiumEngine.Domain.Entities.Bookings;

namespace StadiumEngine.Domain.Entities.Rates;

public class Tariff : BaseRateEntity
{
    public DateTime DateStart { get; set; }
    public DateTime? DateEnd { get; set; }
    public bool Monday { get; set; }
    public bool Tuesday { get; set; }
    public bool Wednesday { get; set; }
    public bool Thursday { get; set; }
    public bool Friday { get; set; }
    public bool Saturday { get; set; }
    public bool Sunday { get; set; }

    public virtual ICollection<TariffDayInterval> TariffDayIntervals { get; set; }
    public virtual ICollection<PromoCode> PromoCodes { get; set; }
    public virtual ICollection<Booking> Bookings { get; set; }
}