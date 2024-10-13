using Mediator;
using Unfield.Common.Enums.Bookings;
using Unfield.DTO.BookingForm;

namespace Unfield.Commands.BookingForm;

public sealed class AddBookingDraftCommand : BaseCommand, IRequest<AddBookingDraftDto>
{
    public DateTime Day { get; set; }
    public BookingSource Source { get; set; }
    public decimal Hour { get; set; }
    public int FieldId { get; set; }
    public int TariffId { get; set; }
}