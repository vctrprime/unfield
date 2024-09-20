using StadiumEngine.Common;
using StadiumEngine.Common.Enums.Notifications;
using StadiumEngine.Common.Exceptions;
using StadiumEngine.Domain.Entities.Customers;
using StadiumEngine.Domain.Repositories.Customers;
using StadiumEngine.Domain.Services.Core.Customers;
using StadiumEngine.Domain.Services.Core.Notifications;
using StadiumEngine.Domain.Services.Models.Customers;
using StadiumEngine.Services.Facades.Accounts;
using StadiumEngine.Services.Facades.Customers;

namespace StadiumEngine.Services.Core.Customers;

internal class CustomerCommandService : ICustomerCommandService
{
    private readonly IUserServiceFacade _userServiceFacade;
    private readonly INotificationsQueueManager _notificationsQueueManager;
    private readonly ICustomerFacade _customerFacade;

    public CustomerCommandService(
        IUserServiceFacade userServiceFacade,
        INotificationsQueueManager notificationsQueueManager,
        ICustomerFacade customerFacade )
    {
        _userServiceFacade = userServiceFacade;
        _notificationsQueueManager = notificationsQueueManager;
        _customerFacade = customerFacade;
    }
    
    public async Task<Customer> AuthorizeCustomerAsync( string login, string stadiumToken, string password )
    {
        Customer? customer = await _customerFacade.GetCustomerAsync( login, stadiumToken );

        if ( customer == null )
        {
            throw new DomainException( ErrorsKeys.InvalidLogin );
        }

        if ( !_userServiceFacade.CheckPassword( customer.Password, password ) )
        {
            throw new DomainException( ErrorsKeys.InvalidPassword );
        }

        customer.LastLoginDate = DateTime.Now.ToUniversalTime();

        _customerFacade.UpdateCustomer( customer );

        return customer;
    }

    public async Task ChangeLanguageAsync( int customerId, string language )
    {
        Customer? customer = await _customerFacade.GetCustomerAsync( customerId );

        if ( customer == null )
        {
            throw new DomainException( ErrorsKeys.UserNotFound );
        }

        customer.Language = language;

        _customerFacade.UpdateCustomer( customer );
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

        Customer? customer = await _customerFacade.GetCustomerAsync( customerId );
        if ( customer == null )
        {
            throw new DomainException( ErrorsKeys.UserNotFound );
        }

        if ( !_userServiceFacade.CheckPassword( customer.Password, oldPassword ) )
        {
            throw new DomainException( ErrorsKeys.InvalidPassword );
        }

        customer.Password = _userServiceFacade.CryptPassword( newPassword );
        _customerFacade.UpdateCustomer( customer );
    }

    public async Task ResetPasswordAsync( string phoneNumber, string stadiumToken )
    {
        phoneNumber = _userServiceFacade.CheckPhoneNumber( phoneNumber );

        Customer? customer = await _customerFacade.GetCustomerAsync( phoneNumber, stadiumToken );
        if ( customer == null )
        {
            throw new DomainException( ErrorsKeys.UserNotFound );
        }

        string customerPassword = _userServiceFacade.GeneratePassword( 8 );
        customer.Password = _userServiceFacade.CryptPassword( customerPassword );

        _customerFacade.UpdateCustomer( customer );
        _notificationsQueueManager.EnqueuePasswordNotification(
            phoneNumber,
            customerPassword,
            customer.Language,
            PasswordNotificationType.Reset,
            PasswordNotificationSubject.Customer,
            customer.StadiumGroup.Name );
    }

    public async Task RegisterAsync( CreateCustomerData createCustomerData )
    {
        Customer? customer = await _customerFacade.GetCustomerAsync(
            createCustomerData.PhoneNumber,
            createCustomerData.Stadium.Id );

        if ( customer != null )
        {
            throw new DomainException( ErrorsKeys.LoginAlreadyExist );
        }

        await _customerFacade.CreateCustomerAsync( createCustomerData );
    }

    public void Update( Customer customer ) => _customerFacade.UpdateCustomer( customer );
}