using AutoMapper;
using Unfield.Commands.Customers;
using Unfield.Domain;
using Unfield.Domain.Services.Core.Customers;
using Unfield.Domain.Services.Models.Customers;
using Unfield.DTO.Customers;

namespace Unfield.Handlers.Handlers.Customers;

internal sealed class AuthorizeCustomerByRedirectHandler : BaseCustomerCommandHandler<AuthorizeCustomerByRedirectCommand, AuthorizeCustomerDto>
{
    private readonly IConfirmBookingRedirectProcessor _confirmBookingRedirectProcessor;

    public AuthorizeCustomerByRedirectHandler(
        IConfirmBookingRedirectProcessor confirmBookingRedirectProcessor,
        IMapper mapper,
        IUnitOfWork unitOfWork
    )
        : base( mapper, null, unitOfWork )
    {
        _confirmBookingRedirectProcessor = confirmBookingRedirectProcessor;
    }

    protected override async ValueTask<AuthorizeCustomerDto> HandleCommandAsync( AuthorizeCustomerByRedirectCommand request,
        CancellationToken cancellationToken )
    {
        ConfirmBookingRedirectResult result = await _confirmBookingRedirectProcessor.ProcessAsync( request.Token, request.Language );

        AuthorizeCustomerDto customerDto = Mapper.Map<AuthorizeCustomerDto>( result.Customer );
        customerDto.Booking = new AuthorizeCustomerBookingDto
        {
            Number = result.BookingNumber,
            StadiumToken = result.BookingStadiumToken
        };

        return customerDto;
    }
}