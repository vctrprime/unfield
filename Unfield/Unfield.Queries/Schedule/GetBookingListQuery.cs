using Mediator;
using Unfield.DTO.Schedule;

namespace Unfield.Queries.Schedule;

public sealed class GetBookingListQuery : BaseQuery, IRequest<List<BookingListItemDto>>
{
    public DateTime? Start { get; set; }
    public DateTime? End { get; set; }
    public string? Language { get; set; }
    public string? BookingNumber { get; set; }
    public int? StadiumId { get; set; }
    public string? CustomerPhoneNumber { get; set; }
}