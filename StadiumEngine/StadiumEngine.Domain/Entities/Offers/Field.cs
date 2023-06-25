#nullable enable
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using StadiumEngine.Common.Enums.Offers;
using StadiumEngine.Domain.Entities.Bookings;
using StadiumEngine.Domain.Entities.Rates;
using StadiumEngine.Domain.Entities.Settings;

namespace StadiumEngine.Domain.Entities.Offers;

[Table( "field", Schema = "offers" )]
public class Field : BaseOfferEntity
{
    [Column( "parent_field_id" )]
    public int? ParentFieldId { get; set; }

    [ForeignKey( "ParentFieldId" )]
    public virtual Field? ParentField { get; set; }

    [Column( "covering_type" )]
    public CoveringType CoveringType { get; set; }

    [Column( "width" )]
    public decimal Width { get; set; }

    [Column( "length" )]
    public decimal Length { get; set; }

    [Column( "price_group_id" )]
    public int? PriceGroupId { get; set; }

    [ForeignKey( "PriceGroupId" )]
    public virtual PriceGroup? PriceGroup { get; set; }

    public virtual ICollection<Field> ChildFields { get; set; } = null!;

    public virtual ICollection<Price>? Prices { get; set; }
    public virtual ICollection<Booking>? Bookings { get; set; }
    public virtual ICollection<BreakField> BreakFields { get; set; } = null!;
}