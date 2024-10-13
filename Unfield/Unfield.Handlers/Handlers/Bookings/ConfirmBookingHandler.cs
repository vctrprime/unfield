using AutoMapper;
using Unfield.Commands.BookingForm;
using Unfield.Common.Enums.Bookings;
using Unfield.Domain;
using Unfield.Domain.Entities.Bookings;
using Unfield.DTO.BookingForm;
using Unfield.Handlers.Facades.Bookings;
using Unfield.MessageContracts.Bookings;

namespace Unfield.Handlers.Handlers.Bookings;

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