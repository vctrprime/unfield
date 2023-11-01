#nullable enable
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using StadiumEngine.Common.Constant;
using StadiumEngine.DTO.BookingForm;
using StadiumEngine.Queries.BookingForm;
using StadiumEngine.WebUI.Infrastructure.Attributes;

namespace StadiumEngine.WebUI.Controllers.API.Bookings;

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
    
    /// <summary>
    ///     Получить данные для формы бронирования при перемещении брони
    /// </summary>
    /// <returns></returns>
    [HttpGet("moving")]
    [HasPermission( PermissionsKeys.UpdateBooking )]
    public async Task<BookingFormDto> Get( [FromQuery] GetBookingFormForMoveQuery query )
    {
        BookingFormDto bookingForm = await Mediator.Send( query );
        return bookingForm;
    }
}