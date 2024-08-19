using MassTransit;

namespace StadiumEngine.EventBus;

internal class EventBusMessageDispatcher: IEventBusMessageDispatcher
{
    private readonly IBus _bus;

    public EventBusMessageDispatcher( IBus bus )
    {
        _bus = bus;
    }

    public Task PublishAsync<TMessage>( TMessage message, CancellationToken cancellationToken ) where TMessage : class => 
        _bus.Publish( message, cancellationToken );
}