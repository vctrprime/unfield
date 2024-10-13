using Unfield.Domain.Services.Core.Customers;
using Unfield.Domain.Services.Models.Schedule;
using Unfield.Services.Facades.Bookings;

namespace Unfield.Services.Core.Customers;

internal class CustomerBookingQueryService : ICustomerBookingQueryService
{
    private readonly IBookingFacade _bookingFacade;

    public CustomerBookingQueryService( IBookingFacade bookingFacade )
    {
        _bookingFacade = bookingFacade;
    }

    public async Task<BookingListItem> GetCustomerBookingAsync(
        string bookingNumber,
        string customerPhoneNumber,
        string stadiumToken,
        DateTime? day ) =>
        await _bookingFacade.GetAsync(
            bookingNumber,
            customerPhoneNumber,
            stadiumToken,
            day );
}