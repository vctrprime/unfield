using AutoMapper;
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

        List<int> stadiumIds = fields.Select( x => x.StadiumId ).ToList();

        Dictionary<int, List<int>> slots =
            await _bookingFormQueryFacade.GetSlotsAsync( stadiumIds );
        List<Price> prices = await _bookingFormQueryFacade.GetPrices( stadiumIds );

        IEnumerable<BookingFormFieldDto> bookingFormFields = fields.Select(
            x => new BookingFormFieldDto
            {
                Data = Mapper.Map<FieldDto>( x ),
                StadiumName = String.IsNullOrEmpty( request.StadiumToken ) ? $"{x.Stadium.Name}, {x.Stadium.Address}" : null,
                Slots = GetSlots(
                    x.Id,
                    slots[ x.StadiumId ],
                    prices,
                    request.Day )
            } );
        BookingFormDto result = new BookingFormDto { Fields = bookingFormFields.Where( x => x.Slots.Any() ).ToList() };

        return result;
    }

    private List<BookingFormFieldSlotDto> GetSlots( int fieldId, List<int> slots, List<Price> prices, DateTime day ) =>
        ( from slot in slots
            let bookingFormPrices = GetPrices(
                fieldId,
                slot,
                prices,
                day )
            //анализ на бронирования
            where bookingFormPrices.Any()
            select new BookingFormFieldSlotDto
            {
                Name = $"{slot:D2}:00",
                Prices = bookingFormPrices
            } ).ToList();

    private static List<BookingFormFieldSlotPriceDto> GetPrices( int fieldId, int slot, List<Price> prices,
        DateTime day )
    {
        List<Price> slotPrices = prices.Where(
            x => x.FieldId == fieldId
                 && ParseDayIntervalPoint( x.TariffDayInterval.DayInterval.Start ) <= slot
                 && ParseDayIntervalPoint( x.TariffDayInterval.DayInterval.End ) > slot
                 && CompareDay( day, x.TariffDayInterval.Tariff )
                 && x.TariffDayInterval.Tariff.DateStart.Date <= day.Date
                 && (x.TariffDayInterval.Tariff.DateEnd?.Date ?? new DateTime( 3000, 1, 1 )) >= day.Date
                 && x.Value > 0 ).ToList();

        return slotPrices.Select(
            x => new BookingFormFieldSlotPriceDto
            {
                TariffName = x.TariffDayInterval.Tariff.Name,
                Value = x.Value
            } ).ToList();
    }

    private static decimal ParseDayIntervalPoint( string point )
    {
        string[] parts = point.Split( ":" );
        if ( parts[ 1 ] == "30" )
        {
            return ( decimal )( Int32.Parse( parts[ 0 ] ) + 0.5 );
        }

        return Int32.Parse( parts[ 0 ] );
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