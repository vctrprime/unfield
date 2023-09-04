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
    
    /// <summary>
    ///     Отменить бронь
    /// </summary>
    /// <returns></returns>
    [HttpDelete( "scheduler-cancel" )]
    [HasPermission( PermissionsKeys.DeleteBooking )]
    public async Task<CancelSchedulerBookingDto> Cancel( [FromBody] CancelSchedulerBookingCommand command )
    {
        CancelSchedulerBookingDto dto = await Mediator.Send( command );
        return dto;
    }
    
    /// <summary>
    ///     Сохранить данные бронирования
    /// </summary>
    /// <returns></returns>
    [HttpPost( "scheduler-save" )]
    [HasPermission( PermissionsKeys.InsertBooking )]
    public async Task<SaveSchedulerBookingDataDto> Save( [FromBody] SaveSchedulerBookingDataCommand command )
    {
        SaveSchedulerBookingDataDto dto = await Mediator.Send( command );
        return dto;
    }
    
    /// <summary>
    ///     Обновить данные бронирования
    /// </summary>
    /// <returns></returns>
    [HttpPut( "scheduler-update" )]
    [HasPermission( PermissionsKeys.UpdateBooking )]
    public async Task<SaveSchedulerBookingDataDto> Update( [FromBody] SaveSchedulerBookingDataCommand command ) => await Save( command );
}