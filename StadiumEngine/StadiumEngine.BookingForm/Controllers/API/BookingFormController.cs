#nullable enable
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using StadiumEngine.DTO.BookingForm;
using StadiumEngine.Queries.BookingForm;

namespace StadiumEngine.BookingForm.Controllers.API;

/// <summary>
///     Данные для формы бронирования
/// </summary>
[Route( "api/booking/form" )]
[AllowAnonymous]
public class BookingFormController : BaseApiController
{
    /// <summary>
    ///     Получить данные для формы бронирования
    /// </summary>
    /// <returns></returns>
    [HttpGet]
    public async Task<BookingFormDto> Get( [FromQuery] GetBookingFormQuery query )
    {
        BookingFormDto bookingForm = await Mediator.Send( query );
        return bookingForm;
    }
}