using Unfield.Domain.Entities.Bookings;
using Unfield.Domain.Entities.Rates;
using Unfield.Domain.Services.Models.BookingForm;

namespace Unfield.Domain.Services.Core.BookingForm;

public interface IBookingCheckoutQueryService
{
    Task<Booking> GetBookingDraftAsync( string bookingNumber );
    Task<Booking> GetConfirmedBookingAsync( string bookingNumber );

    Task<BookingCheckoutData> GetBookingCheckoutDataAsync(
        Booking booking,
        List<BookingCheckoutSlot> slots,
        int? tariffId );

    Task<PromoCode?> CheckPromoAsync( int tariffId, string code );
}