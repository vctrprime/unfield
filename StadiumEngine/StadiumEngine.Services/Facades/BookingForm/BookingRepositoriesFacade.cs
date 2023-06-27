using StadiumEngine.Domain.Entities.Bookings;
using StadiumEngine.Domain.Repositories.Bookings;

namespace StadiumEngine.Services.Facades.BookingForm;

internal class BookingRepositoriesFacade : IBookingRepositoriesFacade
{
    private readonly IBookingRepository _bookingRepository;
    private readonly IBookingCostRepository _bookingCostRepository;
    private readonly IBookingCustomerRepository _bookingCustomerRepository;
    private readonly IBookingInventoryRepository _bookingInventoryRepository;

    public BookingRepositoriesFacade(
        IBookingRepository bookingRepository,
        IBookingCostRepository bookingCostRepository,
        IBookingCustomerRepository bookingCustomerRepository,
        IBookingInventoryRepository bookingInventoryRepository )
    {
        _bookingRepository = bookingRepository;
        _bookingCostRepository = bookingCostRepository;
        _bookingCustomerRepository = bookingCustomerRepository;
        _bookingInventoryRepository = bookingInventoryRepository;
    }

    public async Task<Booking?> GetBookingByNumberAsync( string bookingNumber ) =>
        await _bookingRepository.GetByNumberAsync( bookingNumber );

    public void UpdateBooking( Booking booking ) => _bookingRepository.Update( booking );

    public void AddBookingCosts( IEnumerable<BookingCost> costs ) => _bookingCostRepository.Add( costs );

    public void AddBookingCustomer( BookingCustomer customer ) => _bookingCustomerRepository.Add( customer );

    public void AddBookingInventories( IEnumerable<BookingInventory> inventories ) =>
        _bookingInventoryRepository.Add( inventories );
}