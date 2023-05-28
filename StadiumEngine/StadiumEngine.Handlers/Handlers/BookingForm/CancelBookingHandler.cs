using AutoMapper;
using StadiumEngine.Commands.BookingForm;
using StadiumEngine.Domain;
using StadiumEngine.Domain.Services.Facades.BookingForm;
using StadiumEngine.DTO.BookingForm;

namespace StadiumEngine.Handlers.Handlers.BookingForm;

internal sealed class CancelBookingHandler : BaseCommandHandler<CancelBookingCommand, CancelBookingDto>
{
    private readonly IBookingCheckoutCommandFacade _facade;

    public CancelBookingHandler(
        IBookingCheckoutCommandFacade facade,
        IMapper mapper,
        IUnitOfWork unitOfWork ) : base( mapper, null, unitOfWork )
    {
        _facade = facade;
    }

    protected override async ValueTask<CancelBookingDto> HandleCommandAsync(
        CancelBookingCommand request,
        CancellationToken cancellationToken )
    {
        await _facade.CancelBookingAsync( request.BookingNumber );
        return new CancelBookingDto();
    }
}