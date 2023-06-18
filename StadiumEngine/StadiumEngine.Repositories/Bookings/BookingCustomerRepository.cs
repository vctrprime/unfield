using StadiumEngine.Domain.Entities.Bookings;
using StadiumEngine.Domain.Repositories.Bookings;
using StadiumEngine.Repositories.Infrastructure.Contexts;

namespace StadiumEngine.Repositories.Bookings;

internal class BookingCustomerRepository : BaseRepository<BookingCustomer>, IBookingCustomerRepository
{
    public BookingCustomerRepository( MainDbContext context ) : base( context )
    {
    }

    public new void Add( BookingCustomer customer ) => base.Add( customer );
}