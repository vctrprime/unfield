using AutoMapper;
using StadiumEngine.Commands.BookingForm;
using StadiumEngine.Domain;
using StadiumEngine.Domain.Services.Core.BookingForm;
using StadiumEngine.DTO.BookingForm;

namespace StadiumEngine.Handlers.Handlers.Bookings;

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