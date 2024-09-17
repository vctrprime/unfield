using StadiumEngine.Common.Enums.Notifications;
using StadiumEngine.Domain;
using StadiumEngine.Domain.Entities.Customers;
using StadiumEngine.Domain.Repositories.Customers;
using StadiumEngine.Domain.Services.Core.Notifications;
using StadiumEngine.Domain.Services.Models.Customers;
using StadiumEngine.Services.Facades.Accounts;

namespace StadiumEngine.Services.Facades.Customers;

internal class CustomerFacade : ICustomerFacade
{
    private readonly ICustomerRepository _customerRepository;
    private readonly IUserServiceFacade _userServiceFacade;
    private readonly INotificationsQueueManager _notificationsQueueManager;
    private readonly IUnitOfWork _unitOfWork;

    public CustomerFacade(
        ICustomerRepository customerRepository,
        IUserServiceFacade userServiceFacade,
        INotificationsQueueManager notificationsQueueManager,
        IUnitOfWork unitOfWork )
    {
        _customerRepository = customerRepository;
        _userServiceFacade = userServiceFacade;
        _notificationsQueueManager = notificationsQueueManager;
        _unitOfWork = unitOfWork;
    }

    public async Task<Customer?> GetCustomerAsync( string phoneNumber, int stadiumId ) => 
        await _customerRepository.GetAsync( phoneNumber, stadiumId );
    
    public async Task<Customer?> GetCustomerAsync( int id ) => 
        await _customerRepository.GetAsync( id );

    public async Task<Customer> CreateCustomerAsync( CreateCustomerData createCustomerData )
    {
        string password = _userServiceFacade.GeneratePassword( 8 );
        Customer customer = new Customer
        {
            PhoneNumber = createCustomerData.PhoneNumber,
            Language = createCustomerData.Language,
            FirstName = createCustomerData.FirstName,
            LastName = createCustomerData.LastName,
            Password = _userServiceFacade.CryptPassword( password ),
            StadiumGroupId = createCustomerData.Stadium.StadiumGroupId,
            LastLoginDate = DateTime.Now.ToUniversalTime()
        };
        _customerRepository.Add( customer );
        await _unitOfWork.SaveChangesAsync();

        _notificationsQueueManager.EnqueuePasswordNotification(
            customer.PhoneNumber,
            password,
            customer.Language,
            PasswordNotificationType.Created,
            PasswordNotificationSubject.Customer,
            createCustomerData.Stadium.StadiumGroup.Name );

        return customer;
    }

    public void UpdateCustomer( Customer customer ) => 
        _customerRepository.Update( customer );
}