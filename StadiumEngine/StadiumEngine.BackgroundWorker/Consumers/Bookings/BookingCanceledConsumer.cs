using Mediator;
using StadiumEngine.Commands.EventBus.Bookings;
using StadiumEngine.MessageContracts.Bookings;

namespace StadiumEngine.BackgroundWorker.Consumers.Bookings;

public class BookingCanceledConsumer : BaseMessageConsumer<BookingCanceled>
{
    private readonly IMediator _mediator;

    public BookingCanceledConsumer( 
        IServiceScopeFactory serviceScopeFactory, 
        ILogger<BookingCanceledConsumer> logger, 
        IMediator mediator ) : base( serviceScopeFactory, logger )
    {
        _mediator = mediator;
    }


    protected override async Task HandleAsyncImpl( BookingCanceled message, IServiceProvider serviceProvider ) =>
        await _mediator.Send(
            new BookingCanceledCommand
            {
                BookingNumber = message.Number,
                Day = message.Day,
                Reason = message.Reason
            } );
}