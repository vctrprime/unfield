using StadiumEngine.Domain.Entities.Customers;
using StadiumEngine.Domain.Repositories.Customers;
using StadiumEngine.Domain.Services.Core.Customers;

namespace StadiumEngine.Services.Core.Customers;

internal class CustomerQueryService : ICustomerQueryService
{
    private readonly ICustomerRepository _repository;

    public CustomerQueryService( ICustomerRepository repository )
    {
        _repository = repository;
    }

    public async Task<Customer?> GetCustomerAsync( int customerId ) => 
        await _repository.GetAsync( customerId );
}