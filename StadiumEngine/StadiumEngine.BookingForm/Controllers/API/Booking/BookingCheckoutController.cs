#nullable enable
using Microsoft.AspNetCore.Mvc;
using StadiumEngine.DTO.BookingForm;
using StadiumEngine.DTO.Rates.Tariffs;
using StadiumEngine.Queries.BookingForm;

namespace StadiumEngine.BookingForm.Controllers.API.Booking;

/// <summary>
///     Данные для чекаута бронирования
/// </summary>
[Route( "api/booking/checkout" )]
public class BookingCheckoutController : BaseApiController
{
    /// <summary>
    ///     Получить данные для чекаута бронирования
    /// </summary>
    /// <returns></returns>
    [HttpGet]
    public async Task<BookingCheckoutDto> Get( [FromQuery] GetBookingCheckoutQuery query )
    {
        BookingCheckoutDto bookingCheckout = await Mediator.Send( query );
        return bookingCheckout;
    }
    
    /// <summary>
    /// Проверить промокд
    /// </summary>
    /// <param name="query"></param>
    /// <returns></returns>
    [HttpGet("promo/check")]
    public async Task<PromoCodeDto?> CheckPromoCode( [FromQuery] BookingCheckoutCheckPromoCodeQuery query )
    {
        PromoCodeDto? promoCodeDto = await Mediator.Send( query );
        return promoCodeDto;
    }

}