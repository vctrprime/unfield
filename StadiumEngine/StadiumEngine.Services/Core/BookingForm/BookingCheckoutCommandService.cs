using StadiumEngine.Common;
using StadiumEngine.Common.Exceptions;
using StadiumEngine.Domain.Entities.Bookings;
using StadiumEngine.Domain.Services.Core.BookingForm;
using StadiumEngine.Domain.Services.Core.BookingForm.Distributors;
using StadiumEngine.Services.Facades.BookingForm;
using StadiumEngine.Services.Validators.Bookings;

namespace StadiumEngine.Services.Core.BookingForm;

internal class BookingCheckoutCommandService : IBookingCheckoutCommandService
{
    private readonly IBookingRepositoriesFacade _repositoriesFacade;
    private readonly IBookingIntersectionValidator _intersectionValidator;
    private readonly IBookingLockerRoomDistributor _lockerRoomDistributor;

    public BookingCheckoutCommandService(
        IBookingRepositoriesFacade repositoriesFacade,
        IBookingIntersectionValidator intersectionValidator,
        IBookingLockerRoomDistributor lockerRoomDistributor )
    {
        _repositoriesFacade = repositoriesFacade;
        _intersectionValidator = intersectionValidator;
        _lockerRoomDistributor = lockerRoomDistributor;
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

    public async Task<Booking> ConfirmBookingAsync( string bookingNumber, string accessCode )
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

        await _lockerRoomDistributor.DistributeAsync( booking );
        
        return booking;
    }
}