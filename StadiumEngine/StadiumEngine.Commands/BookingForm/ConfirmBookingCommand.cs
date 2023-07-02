using Mediator;
using StadiumEngine.DTO.BookingForm;

namespace StadiumEngine.Commands.BookingForm;

public sealed class ConfirmBookingCommand : BaseCommand, IRequest<ConfirmBookingDto>
{
    public string BookingNumber { get; set; } = null!;
    public string AccessCode { get; set; } = null!;
    public string Language { get; set; } = "ru";
}