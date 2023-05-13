#nullable enable
using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using StadiumEngine.DTO.BookingForm;
using StadiumEngine.Queries.BookingForm;

namespace StadiumEngine.WebUI.Controllers.API.BookingForm;

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
}