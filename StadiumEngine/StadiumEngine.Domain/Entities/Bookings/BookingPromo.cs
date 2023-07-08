using System.ComponentModel.DataAnnotations.Schema;
using StadiumEngine.Common.Enums.Rates;

namespace StadiumEngine.Domain.Entities.Bookings;

[Table( "booking_promo", Schema = "bookings" )]
public class BookingPromo : BaseBookingEntity
{
    [Column( "booking_id" )]
    public int BookingId { get; set; }
    
    [Column( "code" )]
    public string Code { get; set; }
    
    [Column( "type" )]
    public PromoCodeType Type { get; set; }
    
    [Column( "value" )]
    public decimal Value { get; set; }
    
    [ForeignKey( "BookingId" )]
    public virtual Booking Booking { get; set; }
}