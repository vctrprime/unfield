using StadiumEngine.Common;
using StadiumEngine.Common.Enums.Bookings;
using StadiumEngine.Common.Exceptions;
using StadiumEngine.Domain.Entities.Accounts;
using StadiumEngine.Domain.Entities.Bookings;
using StadiumEngine.Domain.Entities.Customers;
using StadiumEngine.Domain.Repositories.Bookings;
using StadiumEngine.Domain.Services.Core.Customers;
using StadiumEngine.Domain.Services.Models.Customers;
using StadiumEngine.Services.Facades.Customers;

namespace StadiumEngine.Services.Core.Customers;

internal class ConfirmBookingRedirectProcessor : IConfirmBookingRedirectProcessor
{
    private readonly IBookingTokenRepository _bookingTokenRepository;
    private readonly ICustomerFacade _customerFacade;

    public ConfirmBookingRedirectProcessor(
        IBookingTokenRepository bookingTokenRepository,
        ICustomerFacade customerFacade )
    {
        _bookingTokenRepository = bookingTokenRepository;
        _customerFacade = customerFacade;
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

        Customer? customer = await _customerFacade.GetCustomerAsync(
            bookingToken.Booking.Customer.PhoneNumber,
            stadium.Id );

        if ( customer is null )
        {
            customer = await _customerFacade.CreateCustomerAsync(
                new CreateCustomerData
                {
                    LastName = bookingToken.Booking.Customer.Name,
                    PhoneNumber = bookingToken.Booking.Customer.PhoneNumber,
                    Language = language,
                    Stadium = stadium
                } );
        }
        else
        {
            customer.LastLoginDate = DateTime.Now.ToUniversalTime();
            _customerFacade.UpdateCustomer( customer );
        }

        string bookingNumber = bookingToken.Booking.Number;
        _bookingTokenRepository.Remove( bookingToken );

        return new ConfirmBookingRedirectResult
        {
            Customer = customer,
            BookingNumber = bookingNumber,
            BookingStadiumId = stadium.Id
        };
    }
}