using StadiumEngine.Domain.Entities.Bookings;
using StadiumEngine.Domain.Entities.Offers;
using StadiumEngine.Domain.Services.Core.BookingForm;
using StadiumEngine.DTO.BookingForm;

namespace StadiumEngine.Handlers.Processors.BookingForm;

internal class MovingBookingFormProcessor : IMovingBookingFormProcessor
{
    private readonly IBookingCheckoutQueryService _checkoutService;

    public MovingBookingFormProcessor( IBookingCheckoutQueryService checkoutService )
    {
        _checkoutService = checkoutService;
    }

    public async Task<BookingFormDto> ProcessAsync( string bookingNumber, BookingFormDto originalBookingFormData )
    {
        Booking booking = await _checkoutService.GetConfirmedBookingAsync( bookingNumber );

        BookingFormDto result = FilterFields( booking, originalBookingFormData );
        result = FilterTariff( booking.TariffId, result );
        result = ProcessSlots( booking.HoursCount, result );

        return result;
    }

    private BookingFormDto FilterFields( Booking booking, BookingFormDto data )
    {
        Field currentBookingField = booking.Field;

        List<int> fieldsIds = new List<int>();
        if ( currentBookingField.PriceGroup != null )
        {
            fieldsIds.AddRange( currentBookingField.PriceGroup.Fields.Select( x => x.Id ) );
        }
        else
        {
            fieldsIds.Add( booking.FieldId );
        }

        data.Fields = data.Fields.Where( x => fieldsIds.Contains( x.Data.Id ) ).ToList();

        return data;
    }

    private BookingFormDto FilterTariff( int tariffId, BookingFormDto data )
    {
        BookingFormDto newData = new BookingFormDto
        {
            Fields = new List<BookingFormFieldDto>()
        };

        foreach ( BookingFormFieldDto field in data.Fields )
        {
            List<BookingFormFieldSlotDto> newSlots = new List<BookingFormFieldSlotDto>();
            foreach ( BookingFormFieldSlotDto slot in field.Slots )
            {
                slot.Prices = slot.Prices.Where( x => x.TariffId == tariffId ).ToList();
                if ( !slot.Prices.Any() )
                {
                    continue;
                }

                newSlots.Add( slot );
            }

            if ( !newSlots.Any() )
            {
                continue;
            }

            field.Slots = newSlots;
            newData.Fields.Add( field );
        }

        return newData;
    }

    private BookingFormDto ProcessSlots( decimal hoursCount, BookingFormDto data )
    {
        BookingFormDto newData = new BookingFormDto
        {
            Fields = new List<BookingFormFieldDto>()
        };
        
        int halfOurPartsCount = ( int )( hoursCount / ( decimal )0.5 );

        foreach ( BookingFormFieldDto field in data.Fields )
        {
            List<BookingFormFieldSlotDto> newSlots = new List<BookingFormFieldSlotDto>();
            foreach ( BookingFormFieldSlotDto slot in field.Slots )
            {
                int index = field.Slots.IndexOf( slot );
                int count = 0;
                for ( int i = index; i < field.Slots.Count; i++ )
                {
                    if ( field.Slots[ i ].Enabled ||
                         ( !field.Slots[ i ].Enabled && field.Slots[ i ].DisabledByMinDuration ) )
                    {
                        count++;
                    }

                    if ( count >= halfOurPartsCount || !field.Slots[ i ].Enabled )
                    {
                        break;
                    }
                }

                if ( count >= halfOurPartsCount )
                {
                    newSlots.Add( slot );
                }
            }

            if ( !newSlots.Any() )
            {
                continue;
            }

            field.Slots = newSlots;
            newData.Fields.Add( field );
        }

        return newData;
    }
}