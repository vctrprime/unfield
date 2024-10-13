using AutoMapper;
using Unfield.DTO.BookingForm;
using Unfield.Handlers.Builders.BookingForm;
using Unfield.Handlers.Processors.BookingForm;
using Unfield.Queries.BookingForm;

namespace Unfield.Handlers.Handlers.Bookings;

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