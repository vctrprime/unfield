using StadiumEngine.Domain.Entities.Bookings;
using StadiumEngine.Domain.Services.Models.Schedule;

namespace StadiumEngine.Services.Facades.Bookings;

internal interface IBookingFacade
{
    Task<List<Booking>> GetAsync( DateTime day, List<int> stadiumsIds );
    Task<List<Booking>> GetAsync( DateTime from, DateTime to, int stadiumId );
    Task<BookingListItem> GetAsync(
        string bookingNumber,
        string customerPhoneNumber,
        string stadiumToken,
        DateTime? day );
    Task<List<BookingListItem>> SearchAllByNumberAsync( string bookingNumber, int stadiumId, DateTime? day = null );
}