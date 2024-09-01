using StadiumEngine.Common;
using StadiumEngine.Common.Enums.Bookings;
using StadiumEngine.Common.Enums.Notifications;
using StadiumEngine.Common.Exceptions;
using StadiumEngine.Domain;
using StadiumEngine.Domain.Entities.Accounts;
using StadiumEngine.Domain.Entities.Bookings;
using StadiumEngine.Domain.Entities.Customers;
using StadiumEngine.Domain.Repositories.Bookings;
using StadiumEngine.Domain.Repositories.Customers;
using StadiumEngine.Domain.Services.Core.Customers;
using StadiumEngine.Domain.Services.Core.Notifications;
using StadiumEngine.Domain.Services.Models.Customers;
using StadiumEngine.Services.Facades.Accounts;

namespace StadiumEngine.Services.Core.Customers;

internal class ConfirmBookingRedirectProcessor : IConfirmBookingRedirectProcessor
{
    private readonly IBookingTokenRepository _bookingTokenRepository;
    private readonly ICustomerRepository _customerRepository;
    private readonly IUserServiceFacade _userServiceFacade;
    private readonly INotificationsQueueManager _notificationsQueueManager;
    private readonly IUnitOfWork _unitOfWork;

    public ConfirmBookingRedirectProcessor(
        IBookingTokenRepository bookingTokenRepository,
        ICustomerRepository customerRepository,
        IUserServiceFacade userServiceFacade,
        INotificationsQueueManager notificationsQueueManager,
        IUnitOfWork unitOfWork )
    {
        _bookingTokenRepository = bookingTokenRepository;
        _customerRepository = customerRepository;
        _userServiceFacade = userServiceFacade;
        _notificationsQueueManager = notificationsQueueManager;
        _unitOfWork = unitOfWork;
    }

    public async Task<ConfirmBookingRedirectResult> ProcessAsync( string token, string language )
    {
        BookingToken? bookingToken = await _bookingTokenRepository.GetAsync(
            token,
            BookingTokenType.RedirectToClientAccountAfterConfirm );

        if ( bookingToken is null )
        {
            throw new DomainException( ErrorsKeys.RedirectTokenNotFound );
        }

        Stadium stadium = bookingToken.Booking.Field.Stadium;

        Customer? customer = await _customerRepository.GetAsync( bookingToken.Booking.Customer.PhoneNumber, stadium.Id );

        if ( customer is null )
        {
            string password = _userServiceFacade.GeneratePassword( 8 );
            customer = new Customer
            {
                PhoneNumber = bookingToken.Booking.Customer.PhoneNumber,
                Language = language,
                LastName = bookingToken.Booking.Customer.Name,
                Password = _userServiceFacade.CryptPassword( password ),
                StadiumGroupId = stadium.LegalId
            };
            _customerRepository.Add( customer );
            await _unitOfWork.SaveChangesAsync();
            
            _notificationsQueueManager.EnqueuePasswordNotification(
                customer.PhoneNumber,
                password,
                customer.Language,
                PasswordNotificationType.Created,
                PasswordNotificationSubject.Customer,
                stadium.Legal.Name );
        }

        string bookingNumber = bookingToken.Booking.Number;
        _bookingTokenRepository.Remove( bookingToken );

        return new ConfirmBookingRedirectResult
        {
            Customer = customer,
            BookingNumber = bookingNumber
        };
    }
}