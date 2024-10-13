using Unfield.Domain.Entities.Customers;

namespace Unfield.Domain.Services.Core.Customers;

public interface ICustomerQueryService
{
    Task<Customer?> GetCustomerAsync( int customerId );
}