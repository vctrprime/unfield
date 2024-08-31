using AutoMapper;
using StadiumEngine.Commands.BookingForm;
using StadiumEngine.Common.Configuration.Sections;
using StadiumEngine.Common.Enums.Bookings;
using StadiumEngine.Domain;
using StadiumEngine.Domain.Entities.Bookings;
using StadiumEngine.Domain.Services.Core.BookingForm;
using StadiumEngine.Domain.Services.Core.BookingForm.Builders;
using StadiumEngine.Domain.Services.Core.Notifications;
using StadiumEngine.Domain.Services.Infrastructure;
using StadiumEngine.DTO.BookingForm;
using StadiumEngine.MessageContracts.Bookings;
using StadiumEngine.Services.Notifications.Builders;

namespace StadiumEngine.Handlers.Handlers.Bookings;

internal sealed class ConfirmBookingHandler : BaseCommandHandler<ConfirmBookingCommand, ConfirmBookingDto>
{
    private readonly IBookingCheckoutCommandService _commandService;
    private readonly ISmsSender _smsSender;
    private readonly IMessagePublisher _messagePublisher;
    private readonly ICustomerAccountRedirectUrlBuilder _customerAccountRedirectUrlBuilder;

    public ConfirmBookingHandler(
        IBookingCheckoutCommandService commandService,
        ISmsSender smsSender,
        IMessagePublisher messagePublisher,
        ICustomerAccountRedirectUrlBuilder customerAccountRedirectUrlBuilder,
        IMapper mapper,
        IUnitOfWork unitOfWork ) : base( mapper, null, unitOfWork )
    {
        _commandService = commandService;
        _smsSender = smsSender;
        _messagePublisher = messagePublisher;
        _customerAccountRedirectUrlBuilder = customerAccountRedirectUrlBuilder;
    }

    protected override async ValueTask<ConfirmBookingDto> HandleCommandAsync(
        ConfirmBookingCommand request,
        CancellationToken cancellationToken )
    {
        Booking booking = await _commandService.ConfirmBookingAsync( request.BookingNumber, request.AccessCode );
        BookingToken? redirectToken =
            booking.Tokens.SingleOrDefault( x => x.Type == BookingTokenType.RedirectToClientAccountAfterConfirm );

        //пока закомментим, непонятно надо ли отсылать инфу о раздевалке, если бронируют сильно заранее
        //await _smsSender.SendBookingConfirmation( booking, request.Language );

        await _messagePublisher.PublishAsync( new BookingConfirmed( booking.Number ), cancellationToken );

        return new ConfirmBookingDto
        {
            RedirectUrl = _customerAccountRedirectUrlBuilder.Build( redirectToken?.Token, request.Language ),
        };
    }
}