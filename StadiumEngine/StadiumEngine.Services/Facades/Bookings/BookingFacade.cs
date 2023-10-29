using StadiumEngine.Domain.Entities.Bookings;
using StadiumEngine.Domain.Repositories.Bookings;
using StadiumEngine.Domain.Services.Models.Schedule;

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
            if ( day < weeklyBooking.Day ||
                 ( weeklyBooking.IsWeeklyStoppedDate.HasValue && day > weeklyBooking.IsWeeklyStoppedDate ) )
            {
                continue;
            }

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
                    Booking weeklyDayBooking = _bookingRepository.DetachedClone( weeklyBooking );
                    weeklyDayBooking.Day = weeklyDayBooking.Day.Add( diff );
                    bookings.Add( weeklyDayBooking );
                }

                date = date.AddDays( 1 );
            }
        }

        return bookings.Where( x => !x.IsCanceled && x.IsConfirmed ).ToList();
    }

    public async Task<List<BookingListItem>> SearchAllByNumberAsync( string bookingNumber, int stadiumId )
    {
        List<Booking> bookings = await _bookingRepository.SearchAllByNumberAsync( bookingNumber, stadiumId );

        List<BookingListItem> result = new List<BookingListItem>();

        foreach ( Booking booking in bookings )
        {
            DateTime? bookingDate = null;
            if ( booking.IsWeekly )
            {
                DateTime date = DateTime.Today;
                while ( bookingDate == null )
                {
                    if ( ( booking.IsWeeklyStoppedDate.HasValue && date > booking.IsWeeklyStoppedDate )
                         || ( booking.Tariff.DateEnd.HasValue && date > booking.Tariff.DateEnd ) )
                    {
                        break;
                    }

                    if ( booking.Day.DayOfWeek == date.DayOfWeek &&
                         booking.WeeklyExcludeDays.FirstOrDefault( x => x.Day == date ) == null )
                    {
                        bookingDate = date;
                    }

                    date = date.AddDays( 1 );
                }
            }
            else
            {
                bookingDate = booking.Day;
            }

            result.Add(
                new BookingListItem
                {
                    Day = bookingDate,
                    ClosedDay = booking.IsWeekly ? 
                        booking.IsWeeklyStoppedDate ?? booking.Tariff.DateEnd  : null,
                    OriginalData = booking
                } );
        }

        return result;
    }
}