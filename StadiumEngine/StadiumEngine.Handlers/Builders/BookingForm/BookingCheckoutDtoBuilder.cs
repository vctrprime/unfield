using AutoMapper;
using StadiumEngine.Common;
using StadiumEngine.Common.Exceptions;
using StadiumEngine.Common.Static;
using StadiumEngine.Domain.Entities.Bookings;
using StadiumEngine.Domain.Services.Core.BookingForm;
using StadiumEngine.Domain.Services.Models.BookingForm;
using StadiumEngine.DTO.BookingForm;
using StadiumEngine.Queries.BookingForm;

namespace StadiumEngine.Handlers.Builders.BookingForm;

internal class BookingCheckoutDtoBuilder : IBookingCheckoutDtoBuilder
{
    private readonly IBookingCheckoutQueryService _service;
    private readonly IBookingFormDtoBuilder _bookingFormDtoBuilder;
    private readonly IMapper _mapper;

    public BookingCheckoutDtoBuilder(
        IBookingCheckoutQueryService service,
        IBookingFormDtoBuilder bookingFormDtoBuilder,
        IMapper mapper )
    {
        _service = service;
        _bookingFormDtoBuilder = bookingFormDtoBuilder;
        _mapper = mapper;
    }

    public async Task<BookingCheckoutDto> BuildAsync( GetBookingCheckoutQuery query )
    {
        Booking booking = await _service.GetBookingDraftAsync( query.BookingNumber );

        BookingFormDto? bookingFormDto =
            await _bookingFormDtoBuilder.BuildAsync( booking.FieldId, booking.Day, query.ClientDate.Hour );

        if ( bookingFormDto == null || !bookingFormDto.Fields.Any() )
        {
            throw new DomainException( ErrorsKeys.BookingError );
        }

        List<BookingCheckoutSlot> slots = bookingFormDto.Fields.First()
            .Slots
            .Where(
                x => ( x.Enabled ||
                       TimePointParser.Parse( x.Name ) == booking.StartHour ||
                       TimePointParser.Parse( x.Name ) == booking.StartHour + ( decimal )0.5 ||
                       ( !x.Enabled && x.DisabledByMinDuration )
                     ) &&
                     TimePointParser.Parse( x.Name ) >= booking.StartHour ).Select(
                x => new BookingCheckoutSlot
                {
                    Hour = TimePointParser.Parse( x.Name ),
                    Prices = x.Prices.Select(
                        p => new BookingCheckoutSlotPrice
                        {
                            TariffId = p.TariffId,
                            Value = p.Value
                        } ).ToList()
                } ).ToList();
        BookingCheckoutData bookingCheckoutData =
            await _service.GetBookingCheckoutDataAsync( booking, slots );

        BookingCheckoutDto? result = _mapper.Map<BookingCheckoutDto>( bookingCheckoutData );

        return result;
    }
}