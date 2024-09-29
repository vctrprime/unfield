using StadiumEngine.Domain.Services.Models.Schedule;

namespace StadiumEngine.Domain.Services.Core.Customers;

public interface ICustomerBookingQueryService
{
    Task<BookingListItem> GetCustomerBookingAsync(
        string bookingNumber,
        string customerPhoneNumber,
        string stadiumToken,
        DateTime? day );
}