using AutoMapper;
using StadiumEngine.Commands.BookingForm;
using StadiumEngine.Domain;
using StadiumEngine.Domain.Services.Facades.BookingForm;
using StadiumEngine.DTO.BookingForm;

namespace StadiumEngine.Handlers.Handlers.BookingForm;

internal sealed class ConfirmBookingHandler : BaseCommandHandler<ConfirmBookingCommand, ConfirmBookingDto>
{
    private readonly IBookingCheckoutCommandFacade _facade;

    public ConfirmBookingHandler(
        IBookingCheckoutCommandFacade facade,
        IMapper mapper,
        IUnitOfWork unitOfWork ) : base( mapper, null, unitOfWork )
    {
        _facade = facade;
    }

    protected override async ValueTask<ConfirmBookingDto> HandleCommandAsync(
        ConfirmBookingCommand request,
        CancellationToken cancellationToken )
    {
        await _facade.ConfirmBookingAsync( request.BookingNumber, request.AccessCode );
        return new ConfirmBookingDto();
    }
}