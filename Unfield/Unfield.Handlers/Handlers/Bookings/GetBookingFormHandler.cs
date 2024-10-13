using AutoMapper;
using Mediator;
using Unfield.Common.Enums.Bookings;
using Unfield.DTO.BookingForm;
using Unfield.DTO.Customers;
using Unfield.Handlers.Builders.BookingForm;
using Unfield.Handlers.Resolvers.Customers;
using Unfield.Queries.BookingForm;
using Unfield.Queries.Customers;

namespace Unfield.Handlers.Handlers.Bookings;

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