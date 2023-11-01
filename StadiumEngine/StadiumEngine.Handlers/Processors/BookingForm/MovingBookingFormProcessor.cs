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
        //отфильтровать (только то же поле или из одной ценовой группы, только тот же тариф) и задизайблить дополнительно слоты (не подходящие по длительности)

        //1получили бронь
        Booking booking = await _checkoutService.GetConfirmedBookingAsync( bookingNumber );

        BookingFormDto result = FilterFields( booking, originalBookingFormData );
        result = FilterTariff( booking.TariffId, result );
        result = ProcessSlots( booking.HoursCount, result );
        
        return result;
    }

    private BookingFormDto FilterFields( Booking booking, BookingFormDto data )
    {
        //2 взяли площадки - отфильтровали площадки из original только по той ценовой группе что в площадке брони (либо только та же площадка)
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

    private BookingFormDto FilterTariff(int tariffId, BookingFormDto data)
    {
        //3 убрали все несовпадающие тарифы из слотов
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

            if ( newSlots.Any() )
            {
                field.Slots = newSlots;
                newData.Fields.Add( field );
            }
        }

        return newData;
    }

    private BookingFormDto ProcessSlots( decimal hoursCount, BookingFormDto data )
    {
        //4 идем по слотам и оставляем только те, где подряд встречается необходимое число половинок (в зависимости от starthour из booking)
        
    }
}