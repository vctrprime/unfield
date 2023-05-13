using Mediator;
using StadiumEngine.DTO.BookingForm;

namespace StadiumEngine.Queries.BookingForm;

public sealed class GetBookingFormQuery : IRequest<BookingFormDto>
{
    public DateTime Day { get; set; }
    public string? StadiumToken { get; set; }
    public int? CityId { get; set; }
    public string? Q { get; set; }
    public int CurrentHour { get; set; }
}