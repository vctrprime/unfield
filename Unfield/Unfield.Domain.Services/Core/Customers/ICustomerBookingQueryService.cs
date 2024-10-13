using Unfield.Domain.Services.Models.Schedule;

namespace Unfield.Domain.Services.Core.Customers;

public interface ICustomerBookingQueryService
{
    Task<BookingListItem> GetCustomerBookingAsync(
        string bookingNumber,
        string customerPhoneNumber,
        string stadiumToken,
        DateTime? day );
}