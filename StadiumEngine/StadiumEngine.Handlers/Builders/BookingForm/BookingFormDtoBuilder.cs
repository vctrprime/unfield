using AutoMapper;
using StadiumEngine.Domain.Services.Core.BookingForm;
using StadiumEngine.Domain.Services.Models.BookingForm;
using StadiumEngine.DTO.BookingForm;
using StadiumEngine.Queries.BookingForm;

namespace StadiumEngine.Handlers.Builders.BookingForm;

internal class BookingFormDtoBuilder : IBookingFormDtoBuilder
{
    private readonly IBookingFormQueryService _bookingFormQueryService;
    private readonly IMapper _mapper;

    public BookingFormDtoBuilder( IBookingFormQueryService bookingFormQueryService, IMapper mapper )
    {
        _bookingFormQueryService = bookingFormQueryService;
        _mapper = mapper;
    }

    public async Task<BookingFormDto> BuildAsync( GetBookingFormQuery query ) =>
        await BuildAsync(
            query.StadiumToken,
            query.CityId,
            null,
            query.Q,
            query.Day,
            query.CurrentHour );

    public async Task<BookingFormDto> BuildAsync( int fieldId, DateTime day, int currentHour ) =>
        await BuildAsync(
            null,
            null,
            fieldId,
            null,
            day,
            currentHour );

    private async Task<BookingFormDto> BuildAsync(
        string? stadiumToken,
        int? cityId,
        int? fieldId,
        string? q,
        DateTime day,
        int currentHour )
    {
        BookingFormData bookingFormData = await _bookingFormQueryService.GetBookingFormDataAsync(
            stadiumToken,
            cityId,
            fieldId,
            q,
            day,
            currentHour );

        if ( !bookingFormData.Fields.Any() )
        {
            return new BookingFormDto();
        }

        BookingFormDto result = _mapper.Map<BookingFormDto>( bookingFormData );

        return result;
    }
}