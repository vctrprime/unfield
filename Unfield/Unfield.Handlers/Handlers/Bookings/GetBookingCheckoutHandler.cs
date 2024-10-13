using AutoMapper;
using Unfield.Common.Enums.Bookings;
using Unfield.DTO.BookingForm;
using Unfield.Handlers.Builders.BookingForm;
using Unfield.Queries.BookingForm;

namespace Unfield.Handlers.Handlers.Bookings;

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