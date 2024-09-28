using AutoMapper;
using StadiumEngine.Commands.BookingForm;
using StadiumEngine.Common.Enums.Bookings;
using StadiumEngine.Domain;
using StadiumEngine.Domain.Entities.Bookings;
using StadiumEngine.DTO.BookingForm;
using StadiumEngine.Handlers.Facades.Bookings;
using StadiumEngine.MessageContracts.Bookings;

namespace StadiumEngine.Handlers.Handlers.Bookings;

internal sealed class ConfirmBookingHandler : BaseCommandHandler<ConfirmBookingCommand, ConfirmBookingDto>
{
    private readonly IConfirmBookingFacade _confirmBookingFacade;

    public ConfirmBookingHandler(
        IConfirmBookingFacade confirmBookingFacade,
        IMapper mapper,
        IUnitOfWork unitOfWork ) : base( mapper, null, unitOfWork )
    {
        _confirmBookingFacade = confirmBookingFacade;
    }

    protected override async ValueTask<ConfirmBookingDto> HandleCommandAsync(
        ConfirmBookingCommand request,
        CancellationToken cancellationToken ) =>
        await _confirmBookingFacade.ConfirmAsync( request );
}