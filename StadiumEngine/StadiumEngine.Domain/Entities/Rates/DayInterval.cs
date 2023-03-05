using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace StadiumEngine.Domain.Entities.Rates;

[Table( "day_interval", Schema = "rates" )]
public class DayInterval : BaseEntity
{
    [Column( "start_id" )]
    public string Start { get; set; }

    [Column( "end_id" )]
    public string End { get; set; }

    [NotMapped]
    public new string Name { get; set; }

    [NotMapped]
    public new string Description { get; set; }

    [NotMapped]
    public new DateTime DateModified { get; set; }

    public virtual ICollection<TariffDayInterval> TariffDayIntervals { get; set; }
}