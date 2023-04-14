using Mediator;
using StadiumEngine.DTO.BookingForm;

namespace StadiumEngine.Queries.BookingForm;

public sealed class GetBookingFormQuery : IRequest<BookingFormDto>
{
    public GetBookingFormQuery( DateTime day, string? stadiumToken, int? cityId )
    {
        Day = day;
        StadiumToken = stadiumToken;
        CityId = cityId;
    }
    public DateTime Day { get; }
    public string? StadiumToken { get; }
    public int? CityId { get; }
}