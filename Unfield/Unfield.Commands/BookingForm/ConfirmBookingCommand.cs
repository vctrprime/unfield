using Mediator;
using Unfield.DTO.BookingForm;

namespace Unfield.Commands.BookingForm;

public sealed class ConfirmBookingCommand : BaseCommand, IRequest<ConfirmBookingDto>
{
    public string BookingNumber { get; set; } = null!;
    public string AccessCode { get; set; } = null!;
    public string Language { get; set; } = "ru";
}