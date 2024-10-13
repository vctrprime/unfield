using Microsoft.AspNetCore.Mvc;
using Unfield.Commands.Customers;
using Unfield.CustomerAccount.Infrastructure.Attributes;
using Unfield.DTO.Customers;
using Unfield.DTO.Schedule;
using Unfield.Queries.Customers;

namespace Unfield.CustomerAccount.Controllers.API;

[Route( "api/bookings" )]
public class BookingController : BaseApiController
{
    /// <summary>
    /// Получить брони по датам
    /// </summary>
    /// <param name="query"></param>
    /// <returns></returns>
    [HttpGet("list")]
    [StadiumCustomerProtect]
    public async Task<List<CustomerBookingListItemDto>> GetList( [FromQuery] GetCustomerBookingListQuery query )
    {
        List<CustomerBookingListItemDto> dto = await Mediator.Send( query );
        return dto;
    }
    
    /// <summary>
    /// Получить бронь по номеру
    /// </summary>
    /// <param name="query"></param>
    /// <returns></returns>
    [HttpGet]
    [StadiumCustomerProtect]
    public async Task<BookingListItemDto> Get( [FromQuery] GetCustomerBookingQuery query )
    {
        BookingListItemDto dto = await Mediator.Send( query );
        return dto;
    }
    
    /// <summary>
    ///     Отменить бронь
    /// </summary>
    /// <returns></returns>
    [HttpDelete]
    [StadiumCustomerProtect]
    public async Task<CancelBookingByCustomerDto> Cancel( CancelBookingByCustomerCommand command )
    {
        CancelBookingByCustomerDto dto = await Mediator.Send( command );
        return dto;
    }
}