using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Unfield.Commands.BookingForm;
using Unfield.Commands.Schedule;
using Unfield.Common.Constant;
using Unfield.Common.Enums.Bookings;
using Unfield.DTO.BookingForm;
using Unfield.DTO.Schedule;
using Unfield.Extranet.Infrastructure.Attributes;
using Unfield.Queries.BookingForm;

namespace Unfield.Extranet.Controllers.API.Schedule;

/// <summary>
///     Данные по бронированиям (шахматка)
/// </summary>
[Route( "api/schedule/booking" )]
public class SchedulerBookingController : BaseApiController
{
    /// <summary>
    /// Создать черновик брони
    /// </summary>
    /// <returns></returns>
    [HttpPost( "draft" )]
    [HasPermission( PermissionsKeys.InsertBooking )]
    public async Task<AddBookingDraftDto> CreateDraft( [FromBody] AddBookingDraftCommand command )
    {
        AddBookingDraftDto dto = await Mediator.Send( command );
        return dto;
    }

    /// <summary>
    ///     Отменить бронь
    /// </summary>
    /// <returns></returns>
    [HttpDelete( "cancel" )]
    [HasPermission( PermissionsKeys.DeleteBooking )]
    public async Task<CancelSchedulerBookingDto> Cancel( CancelSchedulerBookingCommand command )
    {
        CancelSchedulerBookingDto dto = await Mediator.Send( command );
        return dto;
    }

    /// <summary>
    ///     Сохранить данные бронирования
    /// </summary>
    /// <returns></returns>
    [HttpPost( "save" )]
    [HasPermission( PermissionsKeys.InsertBooking )]
    public async Task<SaveSchedulerBookingDataDto> Save(
        [FromBody] SaveSchedulerBookingDataCommand command,
        [FromHeader( Name = "Client-Date" )] DateTime clientDate )
    {
        command.ClientDate = clientDate;
        SaveSchedulerBookingDataDto dto = await Mediator.Send( command );
        return dto;
    }

    /// <summary>
    ///     Обновить данные бронирования
    /// </summary>
    /// <returns></returns>
    [HttpPut( "update" )]
    [HasPermission( PermissionsKeys.UpdateBooking )]
    public async Task<SaveSchedulerBookingDataDto> Update(
        [FromBody] SaveSchedulerBookingDataCommand command,
        [FromHeader( Name = "Client-Date" )] DateTime clientDate ) => await Save( command, clientDate );
    
    /// <summary>
    ///     Получить данные для чекаута бронирования
    /// </summary>
    /// <returns></returns>
    [HttpGet("checkout")]
    [HasPermission( $"{PermissionsKeys.InsertBooking},{PermissionsKeys.UpdateBooking}" )]
    public async Task<BookingCheckoutDto> Get( [FromQuery] GetBookingCheckoutQuery query )
    {
        BookingCheckoutDto bookingCheckout = await Mediator.Send( query );
        return bookingCheckout;
    }
    
    /// <summary>
    ///     Получить данные для формы бронирования
    /// </summary>
    /// <returns></returns>
    [HttpGet("form")]
    [HasPermission( $"{PermissionsKeys.InsertBooking},{PermissionsKeys.UpdateBooking}" )]
    public async Task<BookingFormDto> Get( [FromQuery] GetBookingFormQuery query )
    {
        query.Source = BookingSource.Schedule;
        BookingFormDto bookingForm = await Mediator.Send( query );
        return bookingForm;
    }
    
    /// <summary>
    ///     Получить данные для формы бронирования при перемещении брони
    /// </summary>
    /// <returns></returns>
    [HttpGet("form/moving")]
    [HasPermission( PermissionsKeys.UpdateBooking )]
    public async Task<BookingFormDto> Get( [FromQuery] GetBookingFormForMoveQuery query )
    {
        query.Source = BookingSource.Schedule;
        BookingFormDto bookingForm = await Mediator.Send( query );
        return bookingForm;
    }
}