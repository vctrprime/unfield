using Mediator;
using StadiumEngine.DTO.Rates.Tariffs;

namespace StadiumEngine.Queries.BookingForm;

public sealed class BookingCheckoutCheckPromoCodeQuery : IRequest<PromoCodeDto?>
{
    public int TariffId { get; set; }
    public string Code { get; set; } = null!;
}