using Mediator;
using StadiumEngine.DTO.Customers;
using StadiumEngine.DTO.Schedule;

namespace StadiumEngine.Queries.Customers;

public sealed class GetCustomerBookingListQuery : BaseCustomerQuery, IRequest<List<CustomerBookingListItemDto>>
{
    public DateTime Start { get; set; }
    public DateTime End { get; set; }
    public string? Language { get; set; }
}