using AutoMapper;
using Unfield.Common;
using Unfield.Common.Exceptions;
using Unfield.Domain.Entities.Bookings;
using Unfield.Domain.Services.Core.BookingForm;
using Unfield.Domain.Services.Models.BookingForm;
using Unfield.DTO.BookingForm;
using Unfield.Handlers.Resolvers.Customers;
using Unfield.Queries.BookingForm;

namespace Unfield.Handlers.Builders.BookingForm;

internal class BookingCheckoutDtoBuilder : IBookingCheckoutDtoBuilder
{
    private readonly IBookingCheckoutQueryService _service;
    private readonly IBookingFormDtoBuilder _bookingFormDtoBuilder;
    private readonly IMapper _mapper;
    private readonly IBookingAuthorizedCustomerResolver _authorizedCustomerResolver;

    public BookingCheckoutDtoBuilder(
        IBookingCheckoutQueryService service,
        IBookingFormDtoBuilder bookingFormDtoBuilder,
        IMapper mapper,
        IBookingAuthorizedCustomerResolver authorizedCustomerResolver )
    {
        _service = service;
        _bookingFormDtoBuilder = bookingFormDtoBuilder;
        _mapper = mapper;
        _authorizedCustomerResolver = authorizedCustomerResolver;
    }

    public async Task<BookingCheckoutDto> BuildAsync( GetBookingCheckoutQuery query )
    {
        Booking booking = query.IsConfirmed
            ? await _service.GetConfirmedBookingAsync( query.BookingNumber )
            : await _service.GetBookingDraftAsync( query.BookingNumber );
        
        int stadiumId = booking.Field.StadiumId;
        
        if ( query.IsConfirmed && query.Day.HasValue )
        {
            booking.Day = query.Day.Value;
        }

        BookingFormDto? bookingFormDto =
            await _bookingFormDtoBuilder.BuildAsync(
                query.FieldId ?? booking.FieldId,
                booking.Day,
                query.ClientDate.Hour,
                booking.Number );

        if ( bookingFormDto == null || !bookingFormDto.Fields.Any() )
        {
            throw new DomainException( ErrorsKeys.BookingError );
        }

        List<BookingCheckoutSlot> slots = bookingFormDto.Fields.First()
            .Slots
            .Where(
                x => ( x.Enabled ||
                       x.Hour == ( query.StartHour ?? booking.StartHour ) ||
                       x.Hour == ( query.StartHour ?? booking.StartHour ) + ( decimal )0.5 ||
                       ( !x.Enabled && x.DisabledByMinDuration )
                     ) &&
                     x.Hour >= ( query.StartHour ?? booking.StartHour ) ).Select(
                x => new BookingCheckoutSlot
                {
                    Hour = x.Hour,
                    Prices = x.Prices.Select(
                        p => new BookingCheckoutSlotPrice
                        {
                            TariffId = p.TariffId,
                            Value = p.Value
                        } ).ToList()
                } ).ToList();
        BookingCheckoutData bookingCheckoutData =
            await _service.GetBookingCheckoutDataAsync( booking, slots, query.TariffId );

        BookingCheckoutDto result = _mapper.Map<BookingCheckoutDto>( bookingCheckoutData );

        result.Customer = await _authorizedCustomerResolver.ResolveAsync( booking.Source, stadiumId );

        return result;
    }
}