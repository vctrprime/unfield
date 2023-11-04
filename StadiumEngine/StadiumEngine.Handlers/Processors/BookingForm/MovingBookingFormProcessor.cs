using StadiumEngine.Common.Enums.Offers;
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
        //пока убираем проверку на одну ценовую группу, так как все равно выводим предупреждение о смене стоимости
        //Field currentBookingField = booking.Field;
        List<int> fieldsIds = new List<int>();
        
        List<SportKind> bookingSportKinds = booking.Field.SportKinds.Select( x => x.SportKind ).ToList();
        foreach ( BookingFormFieldDto field in data.Fields )
        {
            if ( field.Data.SportKinds == null )
            {
                continue;
            }

            List<SportKind> fieldSportKinds = field.Data.SportKinds.Select( x => x ).ToList();
            if ( bookingSportKinds.Intersect( fieldSportKinds ).Count() == bookingSportKinds.Count )
            {
                fieldsIds.Add( field.Data.Id );
            }
        }
        
        /*
        if ( currentBookingField.PriceGroup != null )
        {
            List<SportKind> bookingSportKinds = booking.Field.SportKinds.Select( x => x.SportKind ).ToList();
            foreach ( Field field in currentBookingField.PriceGroup.Fields )
            {
                List<SportKind> fieldSportKinds = field.SportKinds.Select( x => x.SportKind ).ToList();
                if ( bookingSportKinds.Intersect( fieldSportKinds ).Count() == bookingSportKinds.Count )
                {
                    fieldsIds.Add( field.Id );
                }
            }
        }
        else
        {
            fieldsIds.Add( booking.FieldId );
        }*/
        
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