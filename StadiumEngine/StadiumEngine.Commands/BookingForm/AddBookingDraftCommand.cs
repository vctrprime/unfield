using Mediator;
using StadiumEngine.Common.Enums.Bookings;
using StadiumEngine.DTO.BookingForm;

namespace StadiumEngine.Commands.BookingForm;

public sealed class AddBookingDraftCommand : BaseCommand, IRequest<AddBookingDraftDto>
{
    public DateTime Day { get; set; }
    public BookingSource Source { get; set; }
    public decimal Hour { get; set; }
    public int FieldId { get; set; }
    public int TariffId { get; set; }
}