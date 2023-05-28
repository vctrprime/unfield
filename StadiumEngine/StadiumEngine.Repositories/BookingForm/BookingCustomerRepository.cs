using StadiumEngine.Domain.Entities.BookingForm;
using StadiumEngine.Domain.Repositories.BookingForm;
using StadiumEngine.Repositories.Infrastructure.Contexts;

namespace StadiumEngine.Repositories.BookingForm;

internal class BookingCustomerRepository : BaseRepository<BookingCustomer>, IBookingCustomerRepository
{
    public BookingCustomerRepository( MainDbContext context ) : base( context )
    {
    }

    public new void Add( BookingCustomer customer ) => base.Add( customer );
}