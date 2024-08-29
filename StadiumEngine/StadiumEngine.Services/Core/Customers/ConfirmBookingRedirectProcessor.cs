using StadiumEngine.Common;
using StadiumEngine.Common.Enums.Bookings;
using StadiumEngine.Common.Exceptions;
using StadiumEngine.Domain.Entities.Bookings;
using StadiumEngine.Domain.Entities.Customers;
using StadiumEngine.Domain.Repositories.Bookings;
using StadiumEngine.Domain.Repositories.Customers;
using StadiumEngine.Domain.Services.Core.Customers;
using StadiumEngine.Domain.Services.Models.Customers;

namespace StadiumEngine.Services.Core.Customers;

internal class ConfirmBookingRedirectProcessor : IConfirmBookingRedirectProcessor
{
    private readonly IBookingTokenRepository _bookingTokenRepository;
    private readonly ICustomerRepository _customerRepository;

    public ConfirmBookingRedirectProcessor( IBookingTokenRepository bookingTokenRepository, ICustomerRepository customerRepository )
    {
        _bookingTokenRepository = bookingTokenRepository;
        _customerRepository = customerRepository;
    }

    public async Task<ConfirmBookingRedirectResult> ProcessAsync( string token )
    {
        BookingToken? bookingToken = await _bookingTokenRepository.GetAsync(
            token,
            BookingTokenType.RedirectToClientAccountAfterConfirm );

        if ( bookingToken is null )
        {
            throw new DomainException( ErrorsKeys.RedirectTokenNotFound );
        }

        Customer? customer = await _customerRepository.GetAsync( bookingToken.Booking.Customer.PhoneNumber );

        if ( customer is null )
        {
            customer = new Customer
            {
                PhoneNumber = bookingToken.Booking.Customer.PhoneNumber,
                Language = "ru"
            };
            _customerRepository.Add( customer );
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