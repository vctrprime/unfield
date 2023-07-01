using AutoMapper;
using StadiumEngine.Commands.BookingForm;
using StadiumEngine.Domain;
using StadiumEngine.Domain.Services.Core.BookingForm;
using StadiumEngine.DTO.BookingForm;

namespace StadiumEngine.Handlers.Handlers.BookingForm;

internal sealed class ConfirmBookingHandler : BaseCommandHandler<ConfirmBookingCommand, ConfirmBookingDto>
{
    private readonly IBookingCheckoutCommandService _commandService;

    public ConfirmBookingHandler(
        IBookingCheckoutCommandService commandService,
        IMapper mapper,
        IUnitOfWork unitOfWork ) : base( mapper, null, unitOfWork )
    {
        _commandService = commandService;
    }

    protected override async ValueTask<ConfirmBookingDto> HandleCommandAsync(
        ConfirmBookingCommand request,
        CancellationToken cancellationToken )
    {
        await _commandService.ConfirmBookingAsync( request.BookingNumber, request.AccessCode );
        return new ConfirmBookingDto();
    }
}