using StadiumEngine.Domain.Entities.Bookings;
using StadiumEngine.Domain.Services.Models.BookingForm;

namespace StadiumEngine.Domain.Services.Application.BookingForm;

public interface IBookingCheckoutQueryService
{
    Task<Booking> GetBookingDraftAsync( string bookingNumber );
    Task<BookingCheckoutData> GetBookingCheckoutDataAsync( Booking booking, List<BookingCheckoutSlot> slots );
}