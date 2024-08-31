using AutoMapper;
using StadiumEngine.Common.Enums.Bookings;
using StadiumEngine.DTO.BookingForm;
using StadiumEngine.Handlers.Builders.BookingForm;
using StadiumEngine.Queries.BookingForm;

namespace StadiumEngine.Handlers.Handlers.Bookings;

internal class GetBookingCheckoutHandler : BaseRequestHandler<GetBookingCheckoutQuery, BookingCheckoutDto>
{
    private readonly IBookingCheckoutDtoBuilder _builder;

    public GetBookingCheckoutHandler( IBookingCheckoutDtoBuilder builder, IMapper mapper ) : base( mapper )
    {
        _builder = builder;
    }

    public override async ValueTask<BookingCheckoutDto> Handle(
        GetBookingCheckoutQuery request,
        CancellationToken cancellationToken )
    {
        BookingCheckoutDto result = await _builder.BuildAsync( request );
        
        return result;
    }
}