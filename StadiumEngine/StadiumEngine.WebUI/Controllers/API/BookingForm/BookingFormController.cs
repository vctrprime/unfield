#nullable enable
using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using StadiumEngine.DTO.BookingForm;
using StadiumEngine.Queries.BookingForm;

namespace StadiumEngine.WebUI.Controllers.API.BookingForm;

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
    public async Task<BookingFormDto> Get( DateTime date, string? token = null, int? cityId = null )
    {
        BookingFormDto bookingForm = await Mediator.Send( new GetBookingFormQuery( date, token, cityId ) );
        return bookingForm;
    }
}