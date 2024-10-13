using Mediator;
using Unfield.Common.Enums.Customers;
using Unfield.DTO.Customers;
using Unfield.DTO.Schedule;

namespace Unfield.Queries.Customers;

public sealed class GetCustomerBookingListQuery : BaseCustomerQuery, IRequest<List<CustomerBookingListItemDto>>
{
    public CustomerBookingListMode Mode { get; set; }
    public string? Language { get; set; }
}