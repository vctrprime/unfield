using AutoMapper;
using Mediator;
using StadiumEngine.Common.Enums.Bookings;
using StadiumEngine.DTO.BookingForm;
using StadiumEngine.Handlers.Builders.BookingForm;
using StadiumEngine.Queries.BookingForm;
using StadiumEngine.Queries.Customers;

namespace StadiumEngine.Handlers.Handlers.Bookings;

internal sealed class GetBookingFormHandler : BaseRequestHandler<GetBookingFormQuery, BookingFormDto>
{
    private readonly IBookingFormDtoBuilder _builder;
    private readonly IMediator _mediator;

    public GetBookingFormHandler( IBookingFormDtoBuilder builder, IMediator mediator, IMapper mapper ) : base( mapper )
    {
        _builder = builder;
        _mediator = mediator;
    }

    public override async ValueTask<BookingFormDto> Handle(
        GetBookingFormQuery request,
        CancellationToken cancellationToken )
    {
        BookingFormDto result = await _builder.BuildAsync( request );

        if ( request.Source == BookingSource.Form )
        {
            result.Customer = await _mediator.Send( new GetAuthorizedCustomerQuery(), cancellationToken );
        }
        
        return result;
    }
}