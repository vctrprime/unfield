using AutoMapper;
using StadiumEngine.Domain.Services.Facades.BookingForm;
using StadiumEngine.Domain.Services.Models.BookingForm;
using StadiumEngine.DTO.BookingForm;
using StadiumEngine.Queries.BookingForm;

namespace StadiumEngine.Handlers.Handlers.BookingForm;

internal sealed class GetBookingFormHandler : BaseRequestHandler<GetBookingFormQuery, BookingFormDto>
{
    private readonly IBookingFormQueryFacade _bookingFormQueryFacade;

    public GetBookingFormHandler( IBookingFormQueryFacade bookingFormQueryFacade, IMapper mapper ) : base( mapper )
    {
        _bookingFormQueryFacade = bookingFormQueryFacade;
    }

    public override async ValueTask<BookingFormDto> Handle(
        GetBookingFormQuery request,
        CancellationToken cancellationToken )
    {
        BookingFormData bookingFormData = await _bookingFormQueryFacade.GetBookingFormDataAsync(
            request.StadiumToken,
            request.CityId,
            request.Q,
            request.Day,
            request.CurrentHour );

        if ( !bookingFormData.Fields.Any() )
        {
            return new BookingFormDto();
        }

        BookingFormDto result = Mapper.Map<BookingFormDto>( bookingFormData );

        return result;
    }
}