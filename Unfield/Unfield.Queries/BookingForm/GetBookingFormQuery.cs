using Mediator;
using Unfield.Common.Enums.Bookings;
using Unfield.DTO.BookingForm;

namespace Unfield.Queries.BookingForm;

public class GetBookingFormQuery : BaseQuery, IRequest<BookingFormDto>
{
    public DateTime Day { get; set; }
    public string? StadiumToken { get; set; }
    public int? CityId { get; set; }
    public string? Q { get; set; }
    public BookingSource Source { get; set; }
}