using AutoMapper;
using StadiumEngine.DTO.BookingForm;
using StadiumEngine.Handlers.Builders.BookingForm;
using StadiumEngine.Queries.BookingForm;

namespace StadiumEngine.Handlers.Handlers.BookingForm;

internal sealed class GetBookingFormHandler : BaseRequestHandler<GetBookingFormQuery, BookingFormDto>
{
    private readonly IBookingFormDtoBuilder _builder;

    public GetBookingFormHandler( IBookingFormDtoBuilder builder, IMapper mapper ) : base( mapper )
    {
        _builder = builder;
    }

    public override async ValueTask<BookingFormDto> Handle(
        GetBookingFormQuery request,
        CancellationToken cancellationToken )
    {
        BookingFormDto result = await _builder.BuildAsync( request );
        return result;
    }
}