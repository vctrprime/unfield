namespace Unfield.EventBus;

public interface IEventBusMessageDispatcher
{
    Task PublishAsync<TMessage>( TMessage message, CancellationToken ct = default( CancellationToken ) ) where TMessage : class;
}