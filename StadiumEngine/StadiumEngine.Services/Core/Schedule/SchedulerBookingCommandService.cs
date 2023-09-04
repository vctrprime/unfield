using StadiumEngine.Common;
using StadiumEngine.Common.Exceptions;
using StadiumEngine.Domain.Entities.Bookings;
using StadiumEngine.Domain.Repositories.Bookings;
using StadiumEngine.Domain.Services.Core.BookingForm.Distributors;
using StadiumEngine.Domain.Services.Core.Schedule;
using StadiumEngine.Services.Validators.Bookings;

namespace StadiumEngine.Services.Core.Schedule;

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

    public void AddExcludeDay( int bookingId, DateTime day, int userId )
    {
        BookingWeeklyExcludeDay excludeDay = new BookingWeeklyExcludeDay
        {
            BookingId = bookingId,
            Day = day,
            UserCreatedId = userId
        };
        _bookingWeeklyExcludeDayRepository.Add( excludeDay );
    }
}