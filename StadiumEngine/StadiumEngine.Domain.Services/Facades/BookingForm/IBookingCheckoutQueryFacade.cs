using StadiumEngine.Domain.Entities.BookingForm;
using StadiumEngine.Domain.Services.Models.BookingForm;

namespace StadiumEngine.Domain.Services.Facades.BookingForm;

public interface IBookingCheckoutQueryFacade
{
    Task<Booking> GetBookingDraftAsync( string bookingNumber );
    Task<BookingCheckoutData> GetBookingCheckoutDataAsync( Booking booking, List<BookingCheckoutSlot> slots );
}