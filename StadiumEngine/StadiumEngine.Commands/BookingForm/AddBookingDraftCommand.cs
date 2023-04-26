using Mediator;
using StadiumEngine.DTO.BookingForm;

namespace StadiumEngine.Commands.BookingForm;

public sealed class AddBookingDraftCommand : IRequest<AddBookingDraftDto>
{
    public DateTime Day { get; set; }
    public string Slot { get; set; } = null!;
    public int FieldId { get; set; }
    public int TariffId { get; set; }
}