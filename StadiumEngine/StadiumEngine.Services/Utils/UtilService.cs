using Microsoft.Extensions.Logging;
using StadiumEngine.Common.Configuration;
using StadiumEngine.Domain.Services.Utils;

namespace StadiumEngine.Services.Utils;

internal class UtilService : IUtilService
{
    private readonly HttpClient _client;
    private readonly ILogger<UtilService> _logger;

    public UtilService( UtilServiceConfig config, ILogger<UtilService> logger )
    {
        _client = new HttpClient
        {
            BaseAddress = new Uri( config.BaseUrl )
        };
        _client.DefaultRequestHeaders.Add( "SE-Utils-Api-Key",config.ApiKey );
        _logger = logger;
    }
    public async Task NewUIMessage( int stadiumId )
    {
        try
        {
            using HttpResponseMessage response = await _client.PostAsync(
                $"utils/notifications/new-ui-message/{stadiumId}",
                null );

            response.EnsureSuccessStatusCode();
        }
        catch ( Exception e )
        {
             _logger.LogError( e, "NewUIMessage error" );   
        }
    }
}