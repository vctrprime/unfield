using StadiumEngine.Domain.Services.Core.Customers;
using StadiumEngine.Domain.Services.Models.Schedule;
using StadiumEngine.Services.Facades.Bookings;

namespace StadiumEngine.Services.Core.Customers;

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