using Unfield.Common;
using Unfield.Common.Enums.Bookings;
using Unfield.Common.Exceptions;
using Unfield.Domain.Entities.Bookings;
using Unfield.Domain.Repositories.Bookings;
using Unfield.Domain.Services.Core.BookingForm;
using Unfield.Domain.Services.Core.BookingForm.Distributors;
using Unfield.Services.Facades.BookingForm;
using Unfield.Services.Validators.Bookings;

namespace Unfield.Services.Core.BookingForm;

internal class BookingCheckoutCommandService : IBookingCheckoutCommandService
{
    private readonly IBookingRepository _bookingRepository;
    private readonly IBookingIntersectionValidator _intersectionValidator;
    private readonly IBookingLockerRoomDistributor _lockerRoomDistributor;

    public BookingCheckoutCommandService(
        IBookingRepository bookingRepository,
        IBookingIntersectionValidator intersectionValidator,
        IBookingLockerRoomDistributor lockerRoomDistributor )
    {
        _bookingRepository = bookingRepository;
        _intersectionValidator = intersectionValidator;
        _lockerRoomDistributor = lockerRoomDistributor;
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

    public async Task FillBookingDataAsync( Booking booking )
    {
        if ( !await _intersectionValidator.ValidateAsync( booking ) )
        {
            throw new DomainException( ErrorsKeys.BookingIntersection );
        }
        
        _bookingRepository.Update( booking );
    }

    public async Task<Booking> ConfirmBookingAsync( string bookingNumber, string accessCode )
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
        
        if ( !await _intersectionValidator.ValidateAsync( booking ) )
        {
            throw new DomainException( ErrorsKeys.BookingIntersection );
        }
        
        booking.IsDraft = false;
        booking.IsConfirmed = true;
        booking.Tokens = new List<BookingToken>
        {
            new BookingToken
            {
                Token = Guid.NewGuid().ToString(),
                Type = BookingTokenType.RedirectToClientAccountAfterConfirm
            }
        };

        _bookingRepository.Update( booking );
        
        return booking;
    }
}