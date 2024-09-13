using StadiumEngine.Common;
using StadiumEngine.Common.Exceptions;
using StadiumEngine.Domain.Entities.Customers;
using StadiumEngine.Domain.Repositories.Customers;
using StadiumEngine.Domain.Services.Core.Customers;
using StadiumEngine.Services.Facades.Accounts;

namespace StadiumEngine.Services.Core.Customers;

internal class CustomerCommandService : ICustomerCommandService
{
    private readonly ICustomerRepository _customerRepository;
    private readonly IUserServiceFacade _userServiceFacade;

    public CustomerCommandService( ICustomerRepository customerRepository, IUserServiceFacade userServiceFacade )
    {
        _customerRepository = customerRepository;
        _userServiceFacade = userServiceFacade;
    }


    public async Task<Customer> AuthorizeCustomerAsync( string login, int stadiumId, string password )
    {
        Customer? customer = await _customerRepository.GetAsync( login, stadiumId );

        if ( customer == null )
        {
            throw new DomainException( ErrorsKeys.InvalidLogin );
        }

        if ( !_userServiceFacade.CheckPassword( customer.Password, password ) )
        {
            throw new DomainException( ErrorsKeys.InvalidPassword );
        }

        customer.LastLoginDate = DateTime.Now.ToUniversalTime();

        _customerRepository.Update( customer );

        return customer;
    }
}