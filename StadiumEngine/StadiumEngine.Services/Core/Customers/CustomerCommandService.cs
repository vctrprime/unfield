using StadiumEngine.Common;
using StadiumEngine.Common.Enums.Notifications;
using StadiumEngine.Common.Exceptions;
using StadiumEngine.Domain.Entities.Customers;
using StadiumEngine.Domain.Repositories.Customers;
using StadiumEngine.Domain.Services.Core.Customers;
using StadiumEngine.Domain.Services.Core.Notifications;
using StadiumEngine.Services.Facades.Accounts;

namespace StadiumEngine.Services.Core.Customers;

internal class CustomerCommandService : ICustomerCommandService
{
    private readonly ICustomerRepository _customerRepository;
    private readonly IUserServiceFacade _userServiceFacade;
    private readonly INotificationsQueueManager _notificationsQueueManager;

    public CustomerCommandService(
        ICustomerRepository customerRepository,
        IUserServiceFacade userServiceFacade,
        INotificationsQueueManager notificationsQueueManager )
    {
        _customerRepository = customerRepository;
        _userServiceFacade = userServiceFacade;
        _notificationsQueueManager = notificationsQueueManager;
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

    public async Task ChangeLanguageAsync( int customerId, string language )
    {
        Customer? customer = await _customerRepository.GetAsync( customerId );

        if ( customer == null )
        {
            throw new DomainException( ErrorsKeys.UserNotFound );
        }

        customer.Language = language;

        _customerRepository.Update( customer );
    }

    public async Task ChangePasswordAsync(
        int customerId,
        string newPassword,
        string oldPassword )
    {
        if ( !_userServiceFacade.ValidatePassword( newPassword ) )
        {
            throw new DomainException( ErrorsKeys.PasswordDoesntMatchConditions );
        }

        Customer? customer = await _customerRepository.GetAsync( customerId );
        if ( customer == null )
        {
            throw new DomainException( ErrorsKeys.UserNotFound );
        }

        if ( !_userServiceFacade.CheckPassword( customer.Password, oldPassword ) )
        {
            throw new DomainException( ErrorsKeys.InvalidPassword );
        }

        customer.Password = _userServiceFacade.CryptPassword( newPassword );
        _customerRepository.Update( customer );
    }

    public async Task ResetPasswordAsync( string phoneNumber, int stadiumId )
    {
        phoneNumber = _userServiceFacade.CheckPhoneNumber( phoneNumber );

        Customer? customer = await _customerRepository.GetAsync( phoneNumber, stadiumId );
        if ( customer == null )
        {
            throw new DomainException( ErrorsKeys.UserNotFound );
        }

        string customerPassword = _userServiceFacade.GeneratePassword( 8 );
        customer.Password = _userServiceFacade.CryptPassword( customerPassword );

        _customerRepository.Update( customer );
        _notificationsQueueManager.EnqueuePasswordNotification(
            phoneNumber,
            customerPassword,
            customer.Language,
            PasswordNotificationType.Reset,
            PasswordNotificationSubject.Customer,
            customer.StadiumGroup.Name );
    }
}