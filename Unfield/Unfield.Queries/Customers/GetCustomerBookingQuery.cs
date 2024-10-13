using Mediator;
using Unfield.DTO.Customers;
using Unfield.DTO.Schedule;

namespace Unfield.Queries.Customers;

public class GetCustomerBookingQuery : BaseCustomerQuery, IRequest<BookingListItemDto>
{
    public string Number { get; set; } = null!;
    public DateTime? Day { get; set; }
}