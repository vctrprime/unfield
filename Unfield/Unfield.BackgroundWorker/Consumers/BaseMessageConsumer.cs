using System.Text.Json;
using MassTransit;

namespace Unfield.BackgroundWorker.Consumers;

public abstract class BaseMessageConsumer<TMessage> : IConsumer<TMessage> where TMessage : class
{
    private readonly IServiceScopeFactory _serviceScopeFactory;
    private readonly ILogger<BaseMessageConsumer<TMessage>> _logger;

    protected BaseMessageConsumer( IServiceScopeFactory serviceScopeFactory, ILogger<BaseMessageConsumer<TMessage>> logger )
    {
        _serviceScopeFactory = serviceScopeFactory;
        _logger = logger;
    }

    public async Task Consume( ConsumeContext<TMessage> context )
    {
        TMessage message = context.Message;

        using IServiceScope serviceScope = _serviceScopeFactory.CreateScope();
        try
        {
            _logger.LogInformation( $"Event bus message received. Message {message.GetType().Name}: {JsonSerializer.Serialize( message )}" );
            await HandleAsyncImpl( message, serviceScope.ServiceProvider );
        }
        catch ( Exception ex )
        {
            string errorMessage =
                $"Event handler exception: {ex.Message}. Message {message.GetType().Name}: {JsonSerializer.Serialize( message )}";
            _logger.LogError( errorMessage, ex );
            throw new Exception( $"{errorMessage}", ex );
        }
    }

    protected abstract Task HandleAsyncImpl( TMessage message, IServiceProvider serviceProvider );
}