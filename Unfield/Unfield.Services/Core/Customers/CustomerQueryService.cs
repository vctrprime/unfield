using Unfield.Domain.Entities.Customers;
using Unfield.Domain.Services.Core.Customers;
using Unfield.Services.Facades.Customers;

namespace Unfield.Services.Core.Customers;

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