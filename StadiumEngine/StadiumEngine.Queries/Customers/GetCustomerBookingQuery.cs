using Mediator;
using StadiumEngine.DTO.Customers;
using StadiumEngine.DTO.Schedule;

namespace StadiumEngine.Queries.Customers;

public class GetCustomerBookingQuery : BaseCustomerQuery, IRequest<BookingListItemDto>
{
    public string Number { get; set; } = null!;
    public DateTime? Day { get; set; }
}