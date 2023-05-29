using StadiumEngine.Common;
using StadiumEngine.Common.Exceptions;
using StadiumEngine.Domain.Entities.BookingForm;
using StadiumEngine.Domain.Repositories.BookingForm;
using StadiumEngine.Domain.Services.Facades.BookingForm;

namespace StadiumEngine.Services.Facades.BookingForm;

internal class BookingCheckoutCommandFacade : IBookingCheckoutCommandFacade
{
    private readonly IBookingRepository _bookingRepository;
    private readonly IBookingCostRepository _bookingCostRepository;
    private readonly IBookingCustomerRepository _bookingCustomerRepository;
    private readonly IBookingInventoryRepository _bookingInventoryRepository;

    public BookingCheckoutCommandFacade(
        IBookingRepository bookingRepository,
        IBookingCostRepository bookingCostRepository,
        IBookingCustomerRepository bookingCustomerRepository,
        IBookingInventoryRepository bookingInventoryRepository )
    {
        _bookingRepository = bookingRepository;
        _bookingCostRepository = bookingCostRepository;
        _bookingCustomerRepository = bookingCustomerRepository;
        _bookingInventoryRepository = bookingInventoryRepository;
    }

    public async Task CancelBookingAsync( string bookingNumber )
    {
        Booking? booking = await _bookingRepository.GetByNumberAsync( bookingNumber );

        if ( booking == null )
        {
            throw new DomainException( ErrorsKeys.BookingNotFound );
        }

        booking.IsCanceled = true;
        _bookingRepository.Update( booking );
    }

    public void FillBookingData( Booking booking )
    {
        booking.Customer.BookingId = booking.Id;
        _bookingCustomerRepository.Add( booking.Customer );

        foreach ( BookingCost cost in booking.Costs )
        {
            cost.BookingId = booking.Id;
        }
        _bookingCostRepository.Add( booking.Costs );
        
        if ( booking.Inventories.Any() )
        {
            foreach ( BookingInventory inventory in booking.Inventories )
            {
                inventory.BookingId = booking.Id;
            }
            _bookingInventoryRepository.Add( booking.Inventories );
        }

        _bookingRepository.Update( booking );
    }

    public async Task ConfirmBookingAsync( string bookingNumber, string accessCode )
    {
        Booking? booking = await _bookingRepository.GetByNumberAsync( bookingNumber );

        if ( booking == null || booking.IsConfirmed )
        {
            throw new DomainException( ErrorsKeys.BookingNotFound );
        }

        if ( booking.AccessCode != accessCode )
        {
            throw new DomainException( ErrorsKeys.InvalidAccessCode );
        }

        booking.IsDraft = false;
        booking.IsConfirmed = true;

        _bookingRepository.Update( booking );
    }
}