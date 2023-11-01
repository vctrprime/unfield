using AutoMapper;
using StadiumEngine.DTO.BookingForm;
using StadiumEngine.Handlers.Builders.BookingForm;
using StadiumEngine.Handlers.Processors.BookingForm;
using StadiumEngine.Queries.BookingForm;

namespace StadiumEngine.Handlers.Handlers.Bookings;

internal sealed class GetBookingFormForMoveHandler : BaseRequestHandler<GetBookingFormForMoveQuery, BookingFormDto>
{
    private readonly IBookingFormDtoBuilder _builder;
    private readonly IMovingBookingFormProcessor _processor;

    public GetBookingFormForMoveHandler( IBookingFormDtoBuilder builder, IMovingBookingFormProcessor processor, IMapper mapper ) : base( mapper )
    {
        _builder = builder;
        _processor = processor;
    }

    public override async ValueTask<BookingFormDto> Handle(
        GetBookingFormForMoveQuery request,
        CancellationToken cancellationToken )
    {
        BookingFormDto result = await _builder.BuildAsync( request );
        result = await _processor.ProcessAsync( request.BookingNumber, result );
        
        return result;
    }
}