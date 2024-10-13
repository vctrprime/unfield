using Mediator;
using Unfield.Commands.EventBus.Bookings;
using Unfield.MessageContracts.Bookings;

namespace Unfield.BackgroundWorker.Consumers.Bookings;

public class BookingConfirmedConsumer : BaseMessageConsumer<BookingConfirmed>
{
    private readonly IMediator _mediator;

    public BookingConfirmedConsumer( 
        IServiceScopeFactory serviceScopeFactory, 
        ILogger<BookingConfirmedConsumer> logger, 
        IMediator mediator ) : base( serviceScopeFactory, logger )
    {
        _mediator = mediator;
    }


    protected override async Task HandleAsyncImpl( BookingConfirmed message, IServiceProvider serviceProvider ) =>
        await _mediator.Send(
            new BookingConfirmedCommand
            {
                BookingNumber = message.Number
            } );
}