using AutoMapper;
using StadiumEngine.Domain.Entities.Rates;
using StadiumEngine.Domain.Services.Application.BookingForm;
using StadiumEngine.DTO.Rates.Tariffs;
using StadiumEngine.Queries.BookingForm;

namespace StadiumEngine.Handlers.Handlers.BookingForm;

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