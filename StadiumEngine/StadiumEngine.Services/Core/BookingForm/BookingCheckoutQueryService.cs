using StadiumEngine.Common;
using StadiumEngine.Common.Exceptions;
using StadiumEngine.Domain.Entities.Bookings;
using StadiumEngine.Domain.Entities.Offers;
using StadiumEngine.Domain.Entities.Rates;
using StadiumEngine.Domain.Repositories.Bookings;
using StadiumEngine.Domain.Repositories.Offers;
using StadiumEngine.Domain.Repositories.Rates;
using StadiumEngine.Domain.Services.Core.BookingForm;
using StadiumEngine.Domain.Services.Models.BookingForm;

namespace StadiumEngine.Services.Core.BookingForm;

internal class BookingCheckoutQueryService : IBookingCheckoutQueryService
{
    private readonly IBookingRepository _bookingRepository;
    private readonly IInventoryRepository _inventoryRepository;
    private readonly IPromoCodeRepository _promoCodeRepository;

    public BookingCheckoutQueryService( IBookingRepository bookingRepository, IInventoryRepository inventoryRepository, IPromoCodeRepository promoCodeRepository )
    {
        _bookingRepository = bookingRepository;
        _inventoryRepository = inventoryRepository;
        _promoCodeRepository = promoCodeRepository;
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
            TariffId = booking.TariffId,
            DurationInventories = await GetInventories( booking, durationAmounts.Select( x => x.Duration ) ),
            DurationAmounts = durationAmounts,
            PointPrices = pointPrices
        };
    }

    public async Task<PromoCode?> CheckPromoAsync( int tariffId, string code ) =>
        await _promoCodeRepository.Get( tariffId, code );

    private async Task<List<BookingCheckoutDataDurationInventory>> GetInventories(
        Booking booking,
        IEnumerable<decimal> durations )
    {
        List<BookingCheckoutDataDurationInventory> result = new List<BookingCheckoutDataDurationInventory>();

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

        List<int> notAvailableInventoriesIds = new List<int>();
        Dictionary<Inventory, List<int>> alreadyAnalyzeBookingsIds = new Dictionary<Inventory, List<int>>();

        foreach ( decimal duration in durations )
        {
            BookingCheckoutDataDurationInventory durationInventory = new BookingCheckoutDataDurationInventory
            {
                Duration = duration,
                Inventories = new List<BookingCheckoutDataInventory>()
            };

            foreach ( Inventory inventory in inventories )
            {
                if ( notAvailableInventoriesIds.Contains( inventory.Id ) )
                {
                    continue;
                }

                BookingCheckoutDataInventory checkoutInventory = new BookingCheckoutDataInventory
                {
                    Id = inventory.Id,
                    Name = inventory.Name,
                    Quantity =
                        result.LastOrDefault()?.Inventories.SingleOrDefault( x => x.Id == inventory.Id )?.Quantity ??
                        inventory.Quantity,
                    Price = inventory.Price,
                    Image = inventory.Images.Any() ? inventory.Images.OrderBy( i => i.Order ).First().Path : null
                };

                foreach ( Booking dayBooking in dayBookings )
                {
                    if ( alreadyAnalyzeBookingsIds.ContainsKey( inventory ) &&
                         alreadyAnalyzeBookingsIds[ inventory ].Contains( dayBooking.Id ) )
                    {
                        continue;
                    }

                    bool intersected = dayBooking.StartHour == booking.StartHour ||
                                       dayBooking.StartHour + dayBooking.HoursCount == booking.StartHour + duration ||
                                       ( booking.StartHour > dayBooking.StartHour && booking.StartHour <
                                           dayBooking.StartHour + dayBooking.HoursCount ) ||
                                       ( booking.StartHour + duration > dayBooking.StartHour &&
                                         booking.StartHour + duration <
                                         dayBooking.StartHour + dayBooking.HoursCount );

                    if ( !intersected )
                    {
                        continue;
                    }

                    if ( alreadyAnalyzeBookingsIds.ContainsKey( inventory ) )
                    {
                        alreadyAnalyzeBookingsIds[ inventory ].Add( dayBooking.Id );
                    }
                    else
                    {
                        alreadyAnalyzeBookingsIds[ inventory ] = new List<int>
                        {
                            dayBooking.Id
                        };
                    }

                    BookingInventory? dayBookingInventory =
                        dayBooking.Inventories.FirstOrDefault( x => x.InventoryId == inventory.Id );
                    if ( dayBookingInventory == null )
                    {
                        continue;
                    }

                    checkoutInventory.Quantity -= dayBookingInventory.Quantity;
                    if ( checkoutInventory.Quantity < 1 )
                    {
                        notAvailableInventoriesIds.Add( inventory.Id );
                        break;
                    }
                }

                if ( checkoutInventory.Quantity > 0 )
                {
                    durationInventory.Inventories.Add( checkoutInventory );
                }
            }

            result.Add( durationInventory );
        }

        return result;
    }
}