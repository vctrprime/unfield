using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using StadiumEngine.Commands.BookingForm;
using StadiumEngine.Commands.Schedule;
using StadiumEngine.Common.Constant;
using StadiumEngine.DTO.BookingForm;
using StadiumEngine.DTO.Schedule;
using StadiumEngine.WebUI.Infrastructure.Attributes;

namespace StadiumEngine.WebUI.Controllers.API.Schedule;

/// <summary>
///     Данные по бронированиям (шахматка)
/// </summary>
[Route( "api/booking" )]
public class SchedulerBookingController : BaseApiController
{
    /// <summary>
    /// Создать черновик брони
    /// </summary>
    /// <returns></returns>
    [HttpPost( "scheduler-draft" )]
    [HasPermission( PermissionsKeys.InsertBooking )]
    public async Task<AddBookingDraftDto> CreateDraft( AddBookingDraftCommand command )
    {
        AddBookingDraftDto dto = await Mediator.Send( command );
        return dto;
    }
    
    /*/// <summary>
    ///     Отменить бронь
    /// </summary>
    /// <returns></returns>
    [HttpDelete]
    public async Task<CancelBookingDto> Cancel( [FromBody] CancelBookingCommand command )
    {
        CancelBookingDto dto = await Mediator.Send( command );
        return dto;
    }*/
    
    /// <summary>
    ///     Сохранить данные бронирования
    /// </summary>
    /// <returns></returns>
    [HttpPost]
    public async Task<SaveBookingDataDto> Fill( [FromBody] SaveBookingDataCommand command )
    {
        SaveBookingDataDto dto = await Mediator.Send( command );
        return dto;
    }
    
    /*/// <summary>
    ///     Подтвердить бронь
    /// </summary>
    /// <returns></returns>
    [HttpPut("confirm")]
    public async Task<ConfirmBookingDto> Confirm( [FromBody] ConfirmBookingCommand command )
    {
        ConfirmBookingDto dto = await Mediator.Send( command );
        return dto;
    }*/
}