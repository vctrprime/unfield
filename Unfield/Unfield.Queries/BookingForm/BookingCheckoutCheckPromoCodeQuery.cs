using Mediator;
using Unfield.DTO.Rates.Tariffs;

namespace Unfield.Queries.BookingForm;

public sealed class BookingCheckoutCheckPromoCodeQuery : BaseQuery, IRequest<PromoCodeDto?>
{
    public int TariffId { get; set; }
    public string Code { get; set; } = null!;
}