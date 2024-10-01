using Mediator;
using StadiumEngine.Common.Enums.Customers;
using StadiumEngine.DTO.Customers;
using StadiumEngine.DTO.Schedule;

namespace StadiumEngine.Queries.Customers;

public sealed class GetCustomerBookingListQuery : BaseCustomerQuery, IRequest<List<CustomerBookingListItemDto>>
{
    public CustomerBookingListMode Mode { get; set; }
    public string? Language { get; set; }
}