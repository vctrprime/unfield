using AutoMapper;
using StadiumEngine.Commands.Customers;
using StadiumEngine.Domain;
using StadiumEngine.Domain.Services.Core.Customers;
using StadiumEngine.Domain.Services.Models.Customers;
using StadiumEngine.DTO.Customers;

namespace StadiumEngine.Handlers.Handlers.Customers;

internal sealed class AuthorizeCustomerByRedirectHandler : BaseCommandHandler<AuthorizeCustomerByRedirectCommand, AuthorizeCustomerDto>
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
        customerDto.BookingNumber = result.BookingNumber;

        return customerDto;
    }
}