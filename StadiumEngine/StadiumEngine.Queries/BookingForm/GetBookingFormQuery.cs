using Mediator;
using StadiumEngine.DTO.BookingForm;

namespace StadiumEngine.Queries.BookingForm;

public sealed class GetBookingFormQuery : IRequest<BookingFormDto>
{
    public GetBookingFormQuery( DateTime day, string? stadiumToken, int? cityId, string? q, int currentHour )
    {
        Day = day;
        StadiumToken = stadiumToken;
        CityId = cityId;
        Q = q;
        CurrentHour = currentHour;
    }
    public DateTime Day { get; }
    public string? StadiumToken { get; }
    public int? CityId { get; }
    public string? Q { get; }
    public int CurrentHour { get; }
}