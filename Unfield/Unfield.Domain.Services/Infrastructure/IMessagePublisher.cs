namespace Unfield.Domain.Services.Infrastructure;

public interface IMessagePublisher
{
    Task PublishAsync<TMessage>( TMessage message, CancellationToken ct = default( CancellationToken ) ) where TMessage : class;
}