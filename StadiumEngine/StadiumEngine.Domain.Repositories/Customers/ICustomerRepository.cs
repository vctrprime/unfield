using StadiumEngine.Domain.Entities.Customers;

namespace StadiumEngine.Domain.Repositories.Customers;

public interface ICustomerRepository
{
    Task<Customer?> GetAsync( string phoneNumber );
    void Add( Customer customer );
    void Update( Customer customer );
}