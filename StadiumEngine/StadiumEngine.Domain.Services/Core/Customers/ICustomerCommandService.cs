using StadiumEngine.Domain.Entities.Customers;

namespace StadiumEngine.Domain.Services.Core.Customers;

public interface ICustomerCommandService
{
    Task<Customer> AuthorizeCustomerAsync( string login, int stadiumId, string password );
}