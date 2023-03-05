using System.ComponentModel.DataAnnotations.Schema;
using StadiumEngine.Common.Enums.Offers;
using StadiumEngine.Domain.Entities.Offers;

namespace StadiumEngine.Domain.Entities.Rates;

[Table( "price", Schema = "rates" )]
public class Price : BaseRefEntity
{
    [Column( "field_id" )] public int FieldId { get; set; }

    [ForeignKey( "FieldId" )] public virtual Field Field { get; set; }

    [Column( "tariff_day_interval_id" )] public int TariffDayIntervalId { get; set; }

    [ForeignKey( "TariffDayIntervalId" )] public virtual TariffDayInterval TariffDayInterval { get; set; }

    [Column( "is_obsolete" )] public bool IsObsolete { get; set; }

    [Column( "currency" )] public Currency Currency { get; set; }

    [Column( "value" )] public decimal Value { get; set; }
}