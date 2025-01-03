using Microsoft.Extensions.Logging;
using Unfield.Common.Configuration;
using Unfield.Common.Configuration.Sections;
using Unfield.Domain.Services.Utils;

namespace Unfield.Services.Utils;

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