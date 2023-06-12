using StadiumEngine.Domain.Entities.BookingForm;

namespace StadiumEngine.Services.Facades.Services.BookingForm;

internal interface IBookingRepositoriesFacade
{
    Task<Booking?> GetBookingByNumberAsync( string bookingNumber );
    void UpdateBooking( Booking booking );
    void AddBookingCosts( IEnumerable<BookingCost> costs );
    void AddBookingCustomer( BookingCustomer customer );
    void AddBookingInventories( IEnumerable<BookingInventory> inventories );
}