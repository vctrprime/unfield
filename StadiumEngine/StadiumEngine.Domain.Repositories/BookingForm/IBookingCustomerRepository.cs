using StadiumEngine.Domain.Entities.Bookings;

namespace StadiumEngine.Domain.Repositories.BookingForm;

public interface IBookingCustomerRepository
{
    void Add( BookingCustomer customer );
}