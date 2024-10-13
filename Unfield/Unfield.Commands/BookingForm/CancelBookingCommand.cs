using Mediator;
using Unfield.DTO.BookingForm;

namespace Unfield.Commands.BookingForm;

public sealed class CancelBookingCommand : BaseCommand, IRequest<CancelBookingDto>
{
    public string BookingNumber { get; set; } = null!;
}