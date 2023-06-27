using StadiumEngine.Domain.Entities.Bookings;

namespace StadiumEngine.Services.Facades.Bookings;

internal interface IBookingFacade
{
    Task<List<Booking>> GetAsync( DateTime day, List<int> stadiumsIds );
    Task<List<Booking>> GetAsync( DateTime from, DateTime to, int stadiumId );
}