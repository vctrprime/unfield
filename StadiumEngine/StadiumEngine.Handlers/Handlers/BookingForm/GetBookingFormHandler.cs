using System.Globalization;
using AutoMapper;
using StadiumEngine.Common.Static;
using StadiumEngine.Domain.Entities.BookingForm;
using StadiumEngine.Domain.Entities.Offers;
using StadiumEngine.Domain.Entities.Rates;
using StadiumEngine.Domain.Services.Facades.BookingForm;
using StadiumEngine.DTO.BookingForm;
using StadiumEngine.DTO.Offers.Fields;
using StadiumEngine.Queries.BookingForm;

namespace StadiumEngine.Handlers.Handlers.BookingForm;

internal sealed class GetBookingFormHandler : BaseRequestHandler<GetBookingFormQuery, BookingFormDto>
{
    private readonly IBookingFormQueryFacade _bookingFormQueryFacade;

    public GetBookingFormHandler( IBookingFormQueryFacade bookingFormQueryFacade, IMapper mapper ) : base( mapper )
    {
        _bookingFormQueryFacade = bookingFormQueryFacade;
    }

    public override async ValueTask<BookingFormDto> Handle( GetBookingFormQuery request,
        CancellationToken cancellationToken )
    {
        List<Field> fields = await _bookingFormQueryFacade.GetFieldsForBookingFormAsync(
            request.StadiumToken,
            request.CityId,
            request.Q );

        if ( !fields.Any() )
        {
            return new BookingFormDto();
        }

        List<int> stadiumIds = fields.Select( x => x.StadiumId ).ToList();

        Dictionary<int, List<decimal>> slots =
            await _bookingFormQueryFacade.GetSlotsAsync( stadiumIds );
        List<Price> prices = await _bookingFormQueryFacade.GetPricesAsync( stadiumIds );

        List<Booking> bookings = await _bookingFormQueryFacade.GetBookingsAsync( request.Day, stadiumIds );

        IEnumerable<BookingFormFieldDto> bookingFormFields = fields.Select(
            x =>
            {
                List<BookingFormFieldSlotDto> bookingFormSlots = GetSlots(
                    x.Id,
                    slots[ x.StadiumId ],
                    prices,
                    request.Day,
                    bookings );

                List<BookingFormFieldSlotDto> resultSlots = ( from bookingFormSlot in bookingFormSlots
                    let nextSlotAfterHour =
                        bookingFormSlots.FirstOrDefault(
                            s => TimePointParser.Parse( s.Name ) >= TimePointParser.Parse( bookingFormSlot.Name ) + 1 )
                    where nextSlotAfterHour != null
                    select bookingFormSlot ).ToList();

                return new BookingFormFieldDto
                {
                    Data = Mapper.Map<FieldDto>( x ),
                    StadiumName =
                        String.IsNullOrEmpty( request.StadiumToken ) ? $"{x.Stadium.Name}, {x.Stadium.Address}" : null,
                    Slots = resultSlots
                };
            } );
        BookingFormDto result = new BookingFormDto { Fields = bookingFormFields.Where( x => x.Slots.Any() ).ToList() };

        return result;
    }

    private List<BookingFormFieldSlotDto> GetSlots( int fieldId, List<decimal> slots, List<Price> prices, DateTime day,
        List<Booking> bookings ) =>
        ( from slot in slots
            let bookingFormPrices = GetPrices(
                fieldId,
                slot,
                prices,
                day,
                slots.Max() )
            let booking = bookings.FirstOrDefault(
                x => x.FieldId == fieldId
                     && x.StartHour - ( decimal )0.5 <= slot
                     && x.StartHour + x.HoursCount > slot )
            where bookingFormPrices.Any() && booking == null
            select new BookingFormFieldSlotDto
            {
                Name = TimePointParser.Parse( slot ),
                Prices = bookingFormPrices
            } ).ToList();

    private static List<BookingFormFieldSlotPriceDto> GetPrices( int fieldId, decimal slot, List<Price> prices,
        DateTime day, decimal lastSlot )
    {
        List<Price> slotPrices = prices.Where(
            x => x.FieldId == fieldId
                 && TimePointParser.Parse( x.TariffDayInterval.DayInterval.Start ) <= slot
                 && TimePointParser.Parse( x.TariffDayInterval.DayInterval.End ) >
                 ( slot == lastSlot ? lastSlot - ( decimal )0.1 : slot )
                 && CompareDay( day, x.TariffDayInterval.Tariff )
                 && x.TariffDayInterval.Tariff.DateStart.Date <= day.Date.ToUniversalTime()
                 && ( x.TariffDayInterval.Tariff.DateEnd?.Date ?? new DateTime( 3000, 1, 1 ) ).ToUniversalTime() >=
                 day.Date
                 && x.Value > 0 ).ToList();

        return slotPrices.Select(
            x => new BookingFormFieldSlotPriceDto
            {
                TariffId = x.TariffDayInterval.TariffId,
                TariffName = x.TariffDayInterval.Tariff.Name,
                Value = x.Value
            } ).ToList();
    }

    private static bool CompareDay( DateTime day, Tariff tariff )
    {
        switch ( day.DayOfWeek )
        {
            case DayOfWeek.Monday when tariff.Monday:
            case DayOfWeek.Tuesday when tariff.Tuesday:
            case DayOfWeek.Sunday when tariff.Sunday:
            case DayOfWeek.Wednesday when tariff.Wednesday:
            case DayOfWeek.Thursday when tariff.Thursday:
            case DayOfWeek.Friday when tariff.Friday:
            case DayOfWeek.Saturday when tariff.Saturday:
                return true;
            default:
                return false;
        }
    }
}