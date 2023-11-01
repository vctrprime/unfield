using Mediator;
using StadiumEngine.DTO.BookingForm;

namespace StadiumEngine.Queries.BookingForm;

public class GetBookingFormQuery : BaseQuery, IRequest<BookingFormDto>
{
    public DateTime Day { get; set; }
    public string? StadiumToken { get; set; }
    public int? CityId { get; set; }
    public string? Q { get; set; }
}