using StadiumEngine.Domain.Entities.Customers;
using StadiumEngine.Domain.Services.Core.Customers;
using StadiumEngine.Services.Facades.Customers;

namespace StadiumEngine.Services.Core.Customers;

internal class CustomerQueryService : ICustomerQueryService
{
    private readonly ICustomerFacade _facade;

    public CustomerQueryService( ICustomerFacade facade )
    {
        _facade = facade;
    }

    public async Task<Customer?> GetCustomerAsync( int customerId ) => 
        await _facade.GetCustomerAsync( customerId );
}