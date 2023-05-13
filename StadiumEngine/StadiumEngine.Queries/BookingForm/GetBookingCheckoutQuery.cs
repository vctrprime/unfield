using Mediator;
using StadiumEngine.DTO.BookingForm;

namespace StadiumEngine.Queries.BookingForm;

public sealed class GetBookingCheckoutQuery : IRequest<BookingCheckoutDto>
{
    public string BookingNumber { get; set; } = null!;
    public int CurrentHour { get; set; }
}