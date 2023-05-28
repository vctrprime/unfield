using StadiumEngine.Domain.Entities.BookingForm;

namespace StadiumEngine.Domain.Repositories.BookingForm;

public interface IBookingCustomerRepository
{
    void Add( BookingCustomer customer );
}