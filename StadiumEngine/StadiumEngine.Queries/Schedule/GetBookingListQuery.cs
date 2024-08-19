using Mediator;
using StadiumEngine.DTO.Schedule;

namespace StadiumEngine.Queries.Schedule;

public sealed class GetBookingListQuery : BaseQuery, IRequest<List<BookingListItemDto>>
{
    public DateTime? Start { get; set; }
    public DateTime? End { get; set; }
    public string? Language { get; set; }
    public string? BookingNumber { get; set; }
    public int? StadiumId { get; set; }
}