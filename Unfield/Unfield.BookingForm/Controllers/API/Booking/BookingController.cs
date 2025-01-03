using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Unfield.Commands.BookingForm;
using Unfield.DTO.BookingForm;

namespace Unfield.BookingForm.Controllers.API.Booking;

/// <summary>
///     Данные по бронированиям
/// </summary>
[Route( "api/booking" )]
[AllowAnonymous]
public class BookingController : BaseApiController
{
    /// <summary>
    /// Создать черновик брони
    /// </summary>
    /// <returns></returns>
    [HttpPost( "draft" )]
    public async Task<AddBookingDraftDto> CreateDraft( [FromBody] AddBookingDraftCommand command )
    {
        AddBookingDraftDto dto = await Mediator.Send( command );
        return dto;
    }
    
    /// <summary>
    ///     Отменить бронь
    /// </summary>
    /// <returns></returns>
    [HttpDelete]
    public async Task<CancelBookingDto> Cancel( [FromBody] CancelBookingCommand command )
    {
        CancelBookingDto dto = await Mediator.Send( command );
        return dto;
    }
    
    /// <summary>
    ///     Заполнить бронь данными
    /// </summary>
    /// <returns></returns>
    [HttpPut]
    public async Task<FillBookingDataDto> Fill( [FromBody] FillBookingDataCommand command )
    {
        FillBookingDataDto dto = await Mediator.Send( command );
        return dto;
    }
    
    /// <summary>
    ///     Подтвердить бронь
    /// </summary>
    /// <returns></returns>
    [HttpPut("confirm")]
    public async Task<ConfirmBookingDto> Confirm( [FromBody] ConfirmBookingCommand command )
    {
        ConfirmBookingDto dto = await Mediator.Send( command );
        return dto;
    }
}