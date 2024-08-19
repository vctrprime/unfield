using StadiumEngine.Domain.Services.Infrastructure;
using StadiumEngine.EventBus;

namespace StadiumEngine.Services.Infrastructure;

internal class MessagePublisher : IMessagePublisher
{
    private readonly IEventBusMessageDispatcher _dispatcher;

    public MessagePublisher( IEventBusMessageDispatcher dispatcher )
    {
        _dispatcher = dispatcher;
    }
    
    public async Task PublishAsync<TMessage>( TMessage message, CancellationToken ct = default( CancellationToken ) ) where TMessage : class => 
        await _dispatcher.PublishAsync( message, ct: ct );
}