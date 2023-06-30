using StadiumEngine.Common;
using StadiumEngine.Common.Exceptions;
using StadiumEngine.Domain.Entities.Bookings;
using StadiumEngine.Domain.Entities.Offers;
using StadiumEngine.Domain.Repositories.Bookings;
using StadiumEngine.Domain.Repositories.Offers;
using StadiumEngine.Domain.Services.Application.BookingForm;
using StadiumEngine.Domain.Services.Models.BookingForm;

namespace StadiumEngine.Services.Application.BookingForm;

internal class BookingCheckoutQueryService : IBookingCheckoutQueryService
{
    private readonly IBookingRepository _bookingRepository;
    private readonly IInventoryRepository _inventoryRepository;

    public BookingCheckoutQueryService( IBookingRepository bookingRepository, IInventoryRepository inventoryRepository )
    {
        _bookingRepository = bookingRepository;
        _inventoryRepository = inventoryRepository;
    }

    public async Task<Booking> GetBookingDraftAsync( string bookingNumber )
    {
        Booking? booking = await _bookingRepository.GetByNumberAsync( bookingNumber );

        if ( booking == null || booking.IsCanceled || !booking.IsDraft || booking.IsConfirmed ||
             booking.Customer != null )
        {
            throw new DomainException( ErrorsKeys.BookingNotFound );
        }

        return booking;
    }

    public async Task<BookingCheckoutData> GetBookingCheckoutDataAsync(
        Booking booking,
        List<BookingCheckoutSlot> slots )
    {
        List<BookingCheckoutDataDurationAmount> durationAmounts = new();
        List<BookingCheckoutDataPointPrice> pointPrices = new();

        BookingCheckoutSlot? slot = slots.First();
        decimal amount = 0;
        decimal duration = 0;

        while ( slot != null )
        {
            BookingCheckoutSlotPrice? price = slot.Prices.SingleOrDefault( p => p.TariffId == booking.TariffId ) ??
                                              slot.Prices.FirstOrDefault();
            if ( price == null )
            {
                throw new DomainException( ErrorsKeys.BookingError );
            }

            amount += price.Value / 2;
            duration += ( decimal )0.5;

            if ( duration > ( decimal )0.5 )
            {
                durationAmounts.Add(
                    new BookingCheckoutDataDurationAmount
                    {
                        Duration = duration,
                        Value = amount
                    } );
            }

            pointPrices.Add(
                new BookingCheckoutDataPointPrice
                {
                    Start = slot.Hour,
                    Value = price.Value / 2
                } );

            slot = slots.FirstOrDefault( x => x.Hour == slot.Hour + ( decimal )0.5 );
        }


        return new BookingCheckoutData
        {
            BookingNumber = booking.Number,
            Day = booking.Day.ToString( "dd.MM.yyyy" ),
            Field = booking.Field,
            Tariff = booking.Tariff,
            Inventories = await GetInventory( booking ),
            DurationAmounts = durationAmounts,
            PointPrices = pointPrices
        };
    }

    private async Task<Dictionary<Inventory, decimal>> GetInventory( Booking booking )
    {
        Dictionary<Inventory, decimal> result = new Dictionary<Inventory, decimal>();

        List<Inventory> inventories = await _inventoryRepository.GetAllAsync( booking.Field.StadiumId );
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
                && !x.IsCanceled ).ToList();

        inventories = inventories.Where(
            x => x.IsActive && booking.Field.SportKinds.Select( s => s.SportKind )
                .Intersect( x.SportKinds.Select( s => s.SportKind ) ).Any() ).ToList();

        foreach ( Inventory inventory in inventories )
        {
            result.Add( inventory, inventory.Quantity );

            foreach ( Booking dayBooking in dayBookings )
            {
                if ( !dayBooking.Costs.Any(
                        x => (x.StartHour <= booking.StartHour
                             && booking.StartHour < x.EndHour)
                        || (x.StartHour <= booking.StartHour + 1
                            && booking.StartHour + 1 < x.EndHour)) )
                {
                    continue;
                }

                BookingInventory? dayBookingInventory =
                    dayBooking.Inventories.FirstOrDefault( x => x.InventoryId == inventory.Id );
                if ( dayBookingInventory == null )
                {
                    continue;
                }

                result[ inventory ] -= dayBookingInventory.Quantity;
            }
        }

        return result.Where( x => x.Value > 0 ).ToDictionary( k => k.Key, v => v.Value );
    }
}