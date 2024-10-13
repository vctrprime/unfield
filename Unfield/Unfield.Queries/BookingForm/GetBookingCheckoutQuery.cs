using Mediator;
using Unfield.DTO.BookingForm;

namespace Unfield.Queries.BookingForm;

public sealed class GetBookingCheckoutQuery : BaseQuery, IRequest<BookingCheckoutDto>
{
    public string BookingNumber { get; set; } = null!;
    public bool IsConfirmed { get; set; }
    public int? TariffId { get; set; }
    
    public int? FieldId { get; set; }
    
    public decimal? StartHour { get; set; }
    
    public DateTime? Day { get; set; }
}