using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using StadiumEngine.Commands.BookingForm;
using StadiumEngine.DTO.BookingForm;

namespace StadiumEngine.WebUI.Controllers.API.BookingForm;

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
    public async Task<AddBookingDraftDto> CreateDraft( AddBookingDraftCommand command )
    {
        AddBookingDraftDto dto = await Mediator.Send( command );
        return dto;
    }
}