using StadiumEngine.Domain.Entities.Bookings;

namespace StadiumEngine.Domain.Repositories.Bookings;

public interface IBookingCustomerRepository
{
    void Add( BookingCustomer customer );
}