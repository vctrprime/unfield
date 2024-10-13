using AutoMapper;
using Unfield.Domain.Entities.Rates;
using Unfield.Domain.Services.Core.BookingForm;
using Unfield.DTO.Rates.Tariffs;
using Unfield.Queries.BookingForm;

namespace Unfield.Handlers.Handlers.Bookings;

internal sealed class
    BookingCheckoutCheckPromoCodeHandler : BaseRequestHandler<BookingCheckoutCheckPromoCodeQuery, PromoCodeDto?>
{
    private readonly IBookingCheckoutQueryService _queryService;

    public BookingCheckoutCheckPromoCodeHandler(
        IBookingCheckoutQueryService queryService,
        IMapper mapper ) : base( mapper )
    {
        _queryService = queryService;
    }

    public override async ValueTask<PromoCodeDto?> Handle(
        BookingCheckoutCheckPromoCodeQuery request,
        CancellationToken cancellationToken )
    {
        PromoCode? promoCode = await _queryService.CheckPromoAsync( request.TariffId, request.Code.ToLower() );

        if ( promoCode == null )
        {
            return null;
        }

        PromoCodeDto? dto = Mapper.Map<PromoCodeDto>( promoCode );
        return dto;
    }
}