#nullable enable
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Unfield.DTO.BookingForm;
using Unfield.DTO.Rates.Tariffs;
using Unfield.Queries.BookingForm;

namespace Unfield.BookingForm.Controllers.API.Booking;

/// <summary>
///     Данные для чекаута бронирования
/// </summary>
[Route( "api/booking/checkout" )]
[AllowAnonymous]
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