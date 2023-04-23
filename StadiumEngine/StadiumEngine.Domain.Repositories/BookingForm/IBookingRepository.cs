using StadiumEngine.Domain.Entities.BookingForm;

namespace StadiumEngine.Domain.Repositories.BookingForm;

public interface IBookingRepository
{
    Task<List<Booking>> GetAsync( DateTime day, List<int> stadiumsIds );
}