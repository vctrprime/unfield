using AutoMapper;
using Mediator;
using StadiumEngine.Common.Enums.Bookings;
using StadiumEngine.DTO.BookingForm;
using StadiumEngine.DTO.Customers;
using StadiumEngine.Handlers.Builders.BookingForm;
using StadiumEngine.Handlers.Resolvers.Customers;
using StadiumEngine.Queries.BookingForm;
using StadiumEngine.Queries.Customers;

namespace StadiumEngine.Handlers.Handlers.Bookings;

internal sealed class GetBookingFormHandler : BaseRequestHandler<GetBookingFormQuery, BookingFormDto>
{
    private readonly IBookingFormDtoBuilder _builder;
    private readonly IBookingAuthorizedCustomerResolver _authorizedCustomerResolver;

    public GetBookingFormHandler( IBookingFormDtoBuilder builder, IBookingAuthorizedCustomerResolver authorizedCustomerResolver, IMapper mapper ) : base( mapper )
    {
        _builder = builder;
        _authorizedCustomerResolver = authorizedCustomerResolver;
    }

    public override async ValueTask<BookingFormDto> Handle(
        GetBookingFormQuery request,
        CancellationToken cancellationToken )
    {
        BookingFormDto result = await _builder.BuildAsync( request );

        result.Customer = await _authorizedCustomerResolver.ResolveAsync( request.Source, result.StadiumId );
        
        return result;
    }
}