using Microsoft.AspNetCore.Mvc;
using StadiumEngine.Commands.Customers;
using StadiumEngine.CustomerAccount.Infrastructure.Attributes;
using StadiumEngine.DTO.Customers;
using StadiumEngine.Queries.Customers;

namespace StadiumEngine.CustomerAccount.Controllers.API;

[Route( "api/bookings" )]
public class BookingController : BaseApiController
{
    [HttpGet]
    [StadiumCustomerProtect]
    public async Task<List<string>> Test( [FromRoute] Test query )
    {
        return new List<string>
        {
            "Welcome to Stadium Engine! " + query.StadiumToken,
        };
    }
    
    /// <summary>
    ///     Отменить бронь
    /// </summary>
    /// <returns></returns>
    [HttpDelete( "cancel" )]
    [StadiumCustomerProtect]
    public async Task<CancelBookingByCustomerDto> Cancel( CancelBookingByCustomerCommand command )
    {
        CancelBookingByCustomerDto dto = await Mediator.Send( command );
        return dto;
    }
}

public class Test : BaseCustomerQuery
{
    
}