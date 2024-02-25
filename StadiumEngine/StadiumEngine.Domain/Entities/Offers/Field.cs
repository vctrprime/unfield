#nullable enable
using System.Collections.Generic;
using StadiumEngine.Common.Enums.Offers;
using StadiumEngine.Domain.Entities.Bookings;
using StadiumEngine.Domain.Entities.Rates;
using StadiumEngine.Domain.Entities.Settings;

namespace StadiumEngine.Domain.Entities.Offers;

public class Field : BaseOfferEntity
{
    public int? ParentFieldId { get; set; }
    public CoveringType CoveringType { get; set; }
    public decimal Width { get; set; }
    public decimal Length { get; set; }
    public int? PriceGroupId { get; set; }
    
    public virtual Field? ParentField { get; set; }
    public virtual PriceGroup? PriceGroup { get; set; }

    public virtual ICollection<Field> ChildFields { get; set; } = null!;

    public virtual ICollection<Price>? Prices { get; set; }
    public virtual ICollection<Booking>? Bookings { get; set; }
    public virtual ICollection<BreakField> BreakFields { get; set; } = null!;
}