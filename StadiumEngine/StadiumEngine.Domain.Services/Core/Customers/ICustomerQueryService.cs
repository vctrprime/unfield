using StadiumEngine.Domain.Entities.Customers;

namespace StadiumEngine.Domain.Services.Core.Customers;

public interface ICustomerQueryService
{
    Task<Customer?> GetCustomerAsync( int customerId );
}