using StadiumEngine.Domain.Entities.Bookings;
using StadiumEngine.Domain.Repositories.Bookings;

namespace StadiumEngine.Services.Facades.Bookings;

internal class BookingFacade : IBookingFacade
{
    private readonly IBookingRepository _bookingRepository;

    public BookingFacade( IBookingRepository bookingRepository )
    {
        _bookingRepository = bookingRepository;
    }

    public async Task<List<Booking>> GetAsync( DateTime day, List<int> stadiumsIds )
    {
        List<Booking> bookings = await _bookingRepository.GetAsync( day, stadiumsIds );
        List<Booking> weeklyBookings = await _bookingRepository.GetWeeklyAsync( day, stadiumsIds );

        //добавляем еженедельные бронирования, если совпадает день
        foreach ( Booking weeklyBooking in weeklyBookings.Where(
                     weeklyBooking => weeklyBooking.Day.DayOfWeek == day.DayOfWeek
                                      &&
                                      weeklyBooking.WeeklyExcludeDays.FirstOrDefault( x => x.Day == day ) == null )
                )
        {
            weeklyBooking.Day = day.Date;
            bookings.Add( weeklyBooking );
        }

        return bookings.Where( x => !x.IsCanceled && x.IsConfirmed ).ToList();
    }

    public async Task<List<Booking>> GetAsync( DateTime from, DateTime to, int stadiumId )
    {
        if ( from > to )
        {
            return new List<Booking>();
        }

        List<Booking> bookings = await _bookingRepository.GetAsync( from, to, stadiumId );
        List<Booking> weeklyBookings = await _bookingRepository.GetWeeklyAsync( to, stadiumId );

        //для каждого совпадающего дня добавляем еженедельные бронирования
        foreach ( Booking weeklyBooking in weeklyBookings )
        {
            if ( from < weeklyBooking.Day && to < weeklyBooking.Day )
            {
                continue;
            }
            DateTime date = from.Date;

            while ( date <= to.Date )
            {
                if ( weeklyBooking.IsWeeklyStoppedDate.HasValue && date > weeklyBooking.IsWeeklyStoppedDate )
                {
                    break;
                }
                //день недели совпадает и день не переопределен модификацией
                if ( weeklyBooking.Day.DayOfWeek == date.DayOfWeek &&
                     weeklyBooking.WeeklyExcludeDays.FirstOrDefault( x => x.Day == date ) == null )
                {
                    TimeSpan diff = date.Date - weeklyBooking.Day.Date;
                    weeklyBooking.Day = weeklyBooking.Day.Add( diff );
                    bookings.Add( weeklyBooking );
                }

                date = date.AddDays( 1 );
            }
        }

        return bookings.Where( x => !x.IsCanceled && x.IsConfirmed ).ToList();
    }
}