using AutoMapper;
using StadiumEngine.Commands.BookingForm;
using StadiumEngine.Domain;
using StadiumEngine.Domain.Entities.Bookings;
using StadiumEngine.Domain.Services.Core.BookingForm;
using StadiumEngine.Domain.Services.Infrastructure;
using StadiumEngine.DTO.BookingForm;

namespace StadiumEngine.Handlers.Handlers.BookingForm;

internal sealed class ConfirmBookingHandler : BaseCommandHandler<ConfirmBookingCommand, ConfirmBookingDto>
{
    private readonly IBookingCheckoutCommandService _commandService;
    private readonly ISmsSender _smsSender;

    public ConfirmBookingHandler(
        IBookingCheckoutCommandService commandService,
        ISmsSender smsSender,
        IMapper mapper,
        IUnitOfWork unitOfWork ) : base( mapper, null, unitOfWork )
    {
        _commandService = commandService;
        _smsSender = smsSender;
    }

    protected override async ValueTask<ConfirmBookingDto> HandleCommandAsync(
        ConfirmBookingCommand request,
        CancellationToken cancellationToken )
    {
        Booking booking = await _commandService.ConfirmBookingAsync( request.BookingNumber, request.AccessCode );
        
        //пока закомментим, непонятно надо ли отсылать инфу о раздевалке, если бронируют сильно заранее
        //await _smsSender.SendBookingConfirmation( booking, request.Language );
        
        return new ConfirmBookingDto();
    }
}