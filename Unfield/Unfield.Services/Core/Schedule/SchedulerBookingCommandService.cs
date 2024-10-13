using Unfield.Common;
using Unfield.Common.Exceptions;
using Unfield.Domain.Entities.Bookings;
using Unfield.Domain.Repositories.Bookings;
using Unfield.Domain.Services.Core.BookingForm.Distributors;
using Unfield.Domain.Services.Core.Schedule;
using Unfield.Services.Validators.Bookings;

namespace Unfield.Services.Core.Schedule;

internal class SchedulerBookingCommandService : ISchedulerBookingCommandService
{
    private readonly IBookingRepository _bookingRepository;
    private readonly IBookingIntersectionValidator _intersectionValidator;
    private readonly IBookingLockerRoomDistributor _lockerRoomDistributor;
    private readonly IBookingWeeklyExcludeDayRepository _bookingWeeklyExcludeDayRepository;

    public SchedulerBookingCommandService(
        IBookingRepository bookingRepository,
        IBookingIntersectionValidator intersectionValidator,
        IBookingLockerRoomDistributor lockerRoomDistributor,
        IBookingWeeklyExcludeDayRepository bookingWeeklyExcludeDayRepository )
    {
        _bookingRepository = bookingRepository;
        _intersectionValidator = intersectionValidator;
        _lockerRoomDistributor = lockerRoomDistributor;
        _bookingWeeklyExcludeDayRepository = bookingWeeklyExcludeDayRepository;
    }

    public async Task FillBookingDataAsync( Booking booking, bool autoLockerRoom )
    {
        if ( !await _intersectionValidator.ValidateAsync( booking ) )
        {
            throw new DomainException( ErrorsKeys.BookingIntersection );
        }

        if ( autoLockerRoom )
        {
            await _lockerRoomDistributor.DistributeAsync( booking );
        }

        _bookingRepository.Update( booking );
    }

    public async Task AddVersionAsync( Booking booking )
    {
        if ( !await _intersectionValidator.ValidateAsync( booking ) )
        {
            throw new DomainException( ErrorsKeys.BookingIntersection );
        }

        _bookingRepository.Add( booking );
    }

    public void UpdateOldVersion( Booking booking ) => _bookingRepository.Update( booking );

    public void AddExcludeDay( int bookingId, DateTime day, int? userId, string? reason = null )
    {
        BookingWeeklyExcludeDay excludeDay = new BookingWeeklyExcludeDay
        {
            BookingId = bookingId,
            Day = day,
            UserCreatedId = userId,
            Reason = reason,
            ExcludeByCustomer = userId is null
        };
        _bookingWeeklyExcludeDayRepository.Add( excludeDay );
    }

    public async Task<int> DeleteDraftsByDateAsync( DateTime date, int limit ) => 
        await _bookingRepository.DeleteDraftsByDateAsync( date, limit );
}