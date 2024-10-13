#nullable enable
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Unfield.Common.Enums.Bookings;
using Unfield.DTO.BookingForm;
using Unfield.Queries.BookingForm;

namespace Unfield.BookingForm.Controllers.API.Booking;

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
        query.Source = BookingSource.Form;
        BookingFormDto bookingForm = await Mediator.Send( query );
        return bookingForm;
    }
}