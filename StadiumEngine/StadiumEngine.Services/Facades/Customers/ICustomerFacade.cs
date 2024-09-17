using StadiumEngine.Domain.Entities.Customers;
using StadiumEngine.Domain.Services.Models.Customers;

namespace StadiumEngine.Services.Facades.Customers;

internal interface ICustomerFacade
{
    Task<Customer?> GetCustomerAsync( string phoneNumber, int stadiumId );
    Task<Customer?> GetCustomerAsync( int id );
    Task<Customer> CreateCustomerAsync( CreateCustomerData createCustomerData );
    void UpdateCustomer( Customer customer );
}