using Mediator;
using StadiumEngine.DTO.BookingForm;

namespace StadiumEngine.Queries.BookingForm;

public sealed class GetBookingCheckoutQuery : BaseQuery, IRequest<BookingCheckoutDto>
{
    public string BookingNumber { get; set; } = null!;
    public bool IsConfirmed { get; set; }
}