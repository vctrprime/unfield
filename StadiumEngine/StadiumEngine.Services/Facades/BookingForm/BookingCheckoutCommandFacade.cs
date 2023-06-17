using StadiumEngine.Common;
using StadiumEngine.Common.Exceptions;
using StadiumEngine.Domain.Entities.Bookings;
using StadiumEngine.Domain.Services.Facades.BookingForm;
using StadiumEngine.Services.Facades.Services.BookingForm;
using StadiumEngine.Services.Validators.Bookings;

namespace StadiumEngine.Services.Facades.BookingForm;

internal class BookingCheckoutCommandFacade : IBookingCheckoutCommandFacade
{
    private readonly IBookingRepositoriesFacade _repositoriesFacade;
    private readonly IBookingIntersectionValidator _intersectionValidator;

    public BookingCheckoutCommandFacade(
        IBookingRepositoriesFacade repositoriesFacade,
        IBookingIntersectionValidator intersectionValidator )
    {
        _repositoriesFacade = repositoriesFacade;
        _intersectionValidator = intersectionValidator;
    }

    public async Task CancelBookingAsync( string bookingNumber )
    {
        Booking? booking = await _repositoriesFacade.GetBookingByNumberAsync( bookingNumber );

        if ( booking == null )
        {
            throw new DomainException( ErrorsKeys.BookingNotFound );
        }

        booking.IsCanceled = true;
        _repositoriesFacade.UpdateBooking( booking );
    }

    public async Task FillBookingDataAsync( Booking booking )
    {
        if ( !await _intersectionValidator.Validate( booking ) )
        {
            throw new DomainException( ErrorsKeys.BookingIntersection );
        }
        
        booking.Customer.BookingId = booking.Id;
        _repositoriesFacade.AddBookingCustomer( booking.Customer );

        foreach ( BookingCost cost in booking.Costs )
        {
            cost.BookingId = booking.Id;
        }
        _repositoriesFacade.AddBookingCosts( booking.Costs );
        
        if ( booking.Inventories.Any() )
        {
            foreach ( BookingInventory inventory in booking.Inventories )
            {
                inventory.BookingId = booking.Id;
            }
            _repositoriesFacade.AddBookingInventories( booking.Inventories );
        }

        _repositoriesFacade.UpdateBooking( booking );
    }

    public async Task ConfirmBookingAsync( string bookingNumber, string accessCode )
    {
        Booking? booking = await _repositoriesFacade.GetBookingByNumberAsync( bookingNumber );
        
        if ( booking == null || booking.IsConfirmed )
        {
            throw new DomainException( ErrorsKeys.BookingNotFound );
        }

        if ( booking.AccessCode != accessCode )
        {
            throw new DomainException( ErrorsKeys.InvalidAccessCode );
        }
        
        if ( !await _intersectionValidator.Validate( booking ) )
        {
            throw new DomainException( ErrorsKeys.BookingIntersection );
        }
        
        booking.IsDraft = false;
        booking.IsConfirmed = true;

        _repositoriesFacade.UpdateBooking( booking );
    }
}