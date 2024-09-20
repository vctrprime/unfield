using Microsoft.AspNetCore.Mvc;
using StadiumEngine.CustomerAccount.Infrastructure.Attributes;
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
}

public class Test : BaseCustomerQuery
{
    
}