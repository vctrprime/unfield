using Unfield.Domain.Entities.Customers;
using Unfield.Domain.Services.Models.Customers;

namespace Unfield.Services.Facades.Customers;

internal interface ICustomerFacade
{
    Task<Customer?> GetCustomerAsync( string phoneNumber, int stadiumId );
    Task<Customer?> GetCustomerAsync( string phoneNumber, string stadiumToken );
    Task<Customer?> GetCustomerAsync( int id );
    Task<Customer> CreateCustomerAsync( CreateCustomerData createCustomerData );
    void UpdateCustomer( Customer customer );
}