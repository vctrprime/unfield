using Unfield.Common.Enums.Rates;

namespace Unfield.Domain.Entities.Bookings;

public class BookingPromo : BaseUserEntity
{
    public int BookingId { get; set; }
    public string Code { get; set; }
    public PromoCodeType Type { get; set; }
    public decimal Value { get; set; }
    
    public virtual Booking Booking { get; set; }
}