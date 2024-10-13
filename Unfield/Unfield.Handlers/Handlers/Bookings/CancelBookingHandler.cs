using AutoMapper;
using Unfield.Commands.BookingForm;
using Unfield.Domain;
using Unfield.Domain.Services.Core.BookingForm;
using Unfield.DTO.BookingForm;

namespace Unfield.Handlers.Handlers.Bookings;

internal sealed class CancelBookingHandler : BaseCommandHandler<CancelBookingCommand, CancelBookingDto>
{
    private readonly IBookingCheckoutCommandService _commandService;

    public CancelBookingHandler(
        IBookingCheckoutCommandService commandService,
        IMapper mapper,
        IUnitOfWork unitOfWork ) : base( mapper, null, unitOfWork )
    {
        _commandService = commandService;
    }

    protected override async ValueTask<CancelBookingDto> HandleCommandAsync(
        CancelBookingCommand request,
        CancellationToken cancellationToken )
    {
        await _commandService.CancelBookingAsync( request.BookingNumber );
        return new CancelBookingDto();
    }
}