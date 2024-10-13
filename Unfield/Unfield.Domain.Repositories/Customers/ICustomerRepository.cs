using Unfield.Domain.Entities.Customers;

namespace Unfield.Domain.Repositories.Customers;

public interface ICustomerRepository
{
    Task<Customer?> GetAsync( string phoneNumber, int stadiumId );
    Task<Customer?> GetAsync( string phoneNumber, string stadiumToken );
    Task<Customer?> GetAsync( int id );
    void Add( Customer customer );
    void Update( Customer customer );
}