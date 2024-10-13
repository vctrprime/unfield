using Unfield.Common.Enums.Bookings;
using Unfield.Domain;
using Unfield.Domain.Entities.Bookings;
using Unfield.Domain.Repositories.Bookings;
using Unfield.Domain.Services.Core.BookingForm;

namespace Unfield.Services.Core.BookingForm;

internal class BookingFormCommandService : IBookingFormCommandService
{
    private readonly IBookingRepository _bookingRepository;

    public BookingFormCommandService( IBookingRepository bookingRepository )
    {
        _bookingRepository = bookingRepository;
    }

    public async Task CreateBookingAsync( Booking booking, IUnitOfWork unitOfWork )
    {
        if ( booking.Source == BookingSource.Schedule )
        {
            Booking? existingDraft = await _bookingRepository.FindDraftAsync(
                booking.Day,
                booking.StartHour,
                booking.FieldId );
            if ( existingDraft != null )
            {
                booking.Number = existingDraft.Number;
                return;
            }
        }

        booking.AccessCode = new Random().Next( 1000, 9999 ).ToString();
        booking.IsLastVersion = true;
        booking.Number = "plug";

        _bookingRepository.Add( booking );
        await unitOfWork.SaveChangesAsync();

        booking.Number = $"{booking.Id}-{( booking.Source == BookingSource.Form ? "B" : "S" )}";
        _bookingRepository.Update( booking );
    }
}