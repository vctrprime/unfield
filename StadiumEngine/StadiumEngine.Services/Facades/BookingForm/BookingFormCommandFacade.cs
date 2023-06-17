using StadiumEngine.Common;
using StadiumEngine.Common.Exceptions;
using StadiumEngine.Common.Static;
using StadiumEngine.Domain.Entities.Bookings;
using StadiumEngine.Domain.Entities.Offers;
using StadiumEngine.Domain.Repositories.BookingForm;
using StadiumEngine.Domain.Repositories.Offers;
using StadiumEngine.Domain.Services.Facades.BookingForm;

namespace StadiumEngine.Services.Facades.BookingForm;

internal class BookingFormCommandFacade : IBookingFormCommandFacade
{
    private readonly IBookingRepository _bookingRepository;
    private readonly IFieldRepository _fieldRepository;

    public BookingFormCommandFacade( IBookingRepository bookingRepository, IFieldRepository fieldRepository )
    {
        _bookingRepository = bookingRepository;
        _fieldRepository = fieldRepository;
    }
    public async Task CreateBookingAsync( Booking booking )
    {
        Field? field = await _fieldRepository.GetAsync( booking.FieldId );

        if ( field == null )
        {
            throw new DomainException( ErrorsKeys.FieldNotFound );
        }

        List<Booking> bookings = await _bookingRepository.GetAsync(
            booking.Day,
            new List<int>
            {
                field.StadiumId
            } );
        
        booking.AccessCode = new Random().Next(1000, 9999).ToString();
        booking.Number = $"{booking.Day.ToString("yyyyMMdd")}-{TimePointParser.Parse( booking.StartHour ).Replace( ":", "" )}-{field.StadiumId}-{bookings.Count + 1}";
        
        _bookingRepository.Add( booking );
    }
}