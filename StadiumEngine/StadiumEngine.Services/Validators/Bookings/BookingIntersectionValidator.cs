using StadiumEngine.Domain.Entities.BookingForm;
using StadiumEngine.Domain.Repositories.BookingForm;

namespace StadiumEngine.Services.Validators.Bookings;

internal class BookingIntersectionValidator : IBookingIntersectionValidator
{
    private readonly IBookingRepository _bookingRepository;

    public BookingIntersectionValidator( IBookingRepository bookingRepository )
    {
        _bookingRepository = bookingRepository;
    }

    public async Task<bool> Validate( Booking booking )
    {
        List<Booking> dayBookings = await _bookingRepository.GetAsync(
            booking.Day,
            new List<int>
            {
                booking.Field.StadiumId
            } );

        dayBookings = dayBookings.Where(
            x =>
                x.IsConfirmed
                && x.Id != booking.Id
                && !x.IsCanceled
                && Predicates.RelatedBookingField( x, booking.FieldId ) ).ToList();

        List<BookingCost> dayBookingsCosts = dayBookings.SelectMany( x => x.Costs ).ToList();

        return booking.Costs.All(
            cost =>
                dayBookingsCosts
                    .FirstOrDefault(
                        x =>
                            x.StartHour == cost.StartHour
                            && x.EndHour == cost.EndHour ) == null );
    }
}